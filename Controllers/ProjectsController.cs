using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioBackend.Data;
using PortfolioBackend.Models;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            // Optimized: Added .AsNoTracking() to load projects instantly on the home page
            var projects = await _context.Projects
                .AsNoTracking()
                .Where(x => x.Visible)
                .OrderByDescending(x => x.Featured)
                .ThenBy(x => x.DisplayOrder)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(projects);
        }

        [Authorize]
        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminProjects() => Ok(await _context.Projects
            .AsNoTracking().OrderBy(x => x.DisplayOrder).ThenByDescending(x => x.CreatedAt).ToListAsync());

        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetProjectBySlug(string slug)
        {
            var project = await _context.Projects.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Slug == slug && x.Visible);
            return project == null ? NotFound(new { message = "Project not found." }) : Ok(project);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            // Optimized: Fast single read-only project load using FirstOrDefaultAsync with AsNoTracking
            var project = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.Visible);

            if (project == null)
            {
                return NotFound(new { message = "Project not found." });
            }

            return Ok(project);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project)
        {
            project.CreatedAt = DateTime.UtcNow;
            project.Slug = string.IsNullOrWhiteSpace(project.Slug) ? CreateSlug(project.Title) : CreateSlug(project.Slug);

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project updated)
        {
            // No AsNoTracking here because we need to modify and save
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound(new { message = "Project not found." });
            }

            project.Title = updated.Title;
            project.Slug = string.IsNullOrWhiteSpace(updated.Slug) ? CreateSlug(updated.Title) : CreateSlug(updated.Slug);
            project.Description = updated.Description;
            project.LongDescription = updated.LongDescription;
            project.ImageUrl = updated.ImageUrl;
            project.GithubUrl = updated.GithubUrl;
            project.LiveUrl = updated.LiveUrl;
            project.TechStack = updated.TechStack;
            project.Screenshots = updated.Screenshots;
            project.Featured = updated.Featured;
            project.DisplayOrder = updated.DisplayOrder;
            project.Visible = updated.Visible;
            project.Status = updated.Status;
            project.Problem = updated.Problem;
            project.Solution = updated.Solution;
            project.TechnicalApproach = updated.TechnicalApproach;
            project.KeyFeatures = updated.KeyFeatures;
            project.Challenges = updated.Challenges;
            project.LessonsLearned = updated.LessonsLearned;

            await _context.SaveChangesAsync();

            return Ok(project);
        }

        private static string CreateSlug(string value) => string.Join("-", value.Trim().ToLowerInvariant()
            .Split(new[] { ' ', '_', '/', '\\' }, StringSplitOptions.RemoveEmptyEntries));

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound(new { message = "Project not found." });
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Project deleted successfully." });
        }
    }
}
