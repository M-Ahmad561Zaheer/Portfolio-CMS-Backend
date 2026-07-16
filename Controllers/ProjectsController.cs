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
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            // Optimized: Fast single read-only project load using FirstOrDefaultAsync with AsNoTracking
            var project = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

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
            project.Description = updated.Description;
            project.LongDescription = updated.LongDescription;
            project.ImageUrl = updated.ImageUrl;
            project.GithubUrl = updated.GithubUrl;
            project.LiveUrl = updated.LiveUrl;
            project.TechStack = updated.TechStack;

            await _context.SaveChangesAsync();

            return Ok(project);
        }

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