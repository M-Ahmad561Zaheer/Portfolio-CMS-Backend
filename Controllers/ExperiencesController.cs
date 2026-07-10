using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioBackend.Data;
using PortfolioBackend.Models;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperiencesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExperiencesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetExperiences()
        {
            var experiences = await _context.Experiences
                .OrderBy(x => x.DisplayOrder)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(experiences);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperience(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);

            if (experience == null)
                return NotFound();

            return Ok(experience);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateExperience(Experience experience)
        {
            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();

            return Ok(experience);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperience(int id, Experience updated)
        {
            var experience = await _context.Experiences.FindAsync(id);

            if (experience == null)
                return NotFound();

            experience.Title = updated.Title;
            experience.Company = updated.Company;
            experience.StartDate = updated.StartDate;
            experience.EndDate = updated.EndDate;
            experience.Description = updated.Description;
            experience.DisplayOrder = updated.DisplayOrder;

            await _context.SaveChangesAsync();

            return Ok(experience);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);

            if (experience == null)
                return NotFound();

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Experience deleted successfully." });
        }
    }
}