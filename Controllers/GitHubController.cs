using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace PortfolioBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;

    public GitHubController(
        IHttpClientFactory httpClientFactory,
        IMemoryCache cache,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
        _configuration = configuration;
    }

    [HttpGet("repos")]
    public async Task<IActionResult> GetRecentRepositories(
        [FromQuery] string username,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(username) ||
            username.Length > 39 ||
            username.Any(character => !char.IsLetterOrDigit(character) && character != '-'))
        {
            return BadRequest(new { message = "A valid GitHub username is required." });
        }

        var cacheKey = $"github-repositories:{username.ToLowerInvariant()}";
        if (_cache.TryGetValue(cacheKey, out IReadOnlyList<RepositoryResponse>? cached))
        {
            return Ok(cached);
        }

        var client = _httpClientFactory.CreateClient();
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://api.github.com/users/{Uri.EscapeDataString(username)}/repos?sort=updated&direction=desc&per_page=10&type=owner");
        request.Headers.UserAgent.ParseAdd("AZ-Developers-Portfolio/1.0");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
        request.Headers.Add("X-GitHub-Api-Version", "2022-11-28");

        var token = _configuration["GitHub:Token"] ?? _configuration["GITHUB_TOKEN"];
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        using var response = await client.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                message = "GitHub repositories are temporarily unavailable."
            });
        }

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var repositories = await JsonSerializer.DeserializeAsync<List<GitHubRepository>>(
            stream,
            cancellationToken: cancellationToken) ?? [];

        var result = repositories
            .Where(repository => !repository.Fork)
            .Take(3)
            .Select(repository => new RepositoryResponse(
                repository.Id,
                repository.Name,
                repository.Description,
                repository.HtmlUrl,
                repository.Language,
                repository.UpdatedAt))
            .ToList();

        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30));
        return Ok(result);
    }

    private sealed record RepositoryResponse(
        long Id,
        string Name,
        string? Description,
        string HtmlUrl,
        string? Language,
        DateTimeOffset UpdatedAt);

    private sealed class GitHubRepository
    {
        public long Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; init; } = string.Empty;
        public string? Language { get; init; }
        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; init; }
        public bool Fork { get; init; }
    }
}
