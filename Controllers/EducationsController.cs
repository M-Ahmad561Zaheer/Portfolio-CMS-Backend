using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioBackend.Data;
using PortfolioBackend.Models;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EducationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEducations()
        {
            var educations = await _context.Educations
                .Where(x => x.Visible)
                .OrderBy(x => x.DisplayOrder)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(educations);
        }

        [Authorize]
        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminEducations() => Ok(await _context.Educations.AsNoTracking()
            .OrderBy(x => x.DisplayOrder).ThenByDescending(x => x.CreatedAt).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEducation(int id)
        {
            var education = await _context.Educations.FindAsync(id);

            if (education == null || !education.Visible)
                return NotFound();

            return Ok(education);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEducation(Education education)
        {
            education.Id = 0;
            education.CreatedAt = DateTime.UtcNow;
            if (education.IsCurrent) education.EndDate = "";
            _context.Educations.Add(education);
            await _context.SaveChangesAsync();

            return Ok(education);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEducation(int id, Education updated)
        {
            var education = await _context.Educations.FindAsync(id);

            if (education == null)
                return NotFound();

            education.Degree = updated.Degree;
            education.Institution = updated.Institution;
            education.Location = updated.Location;
            education.StartDate = updated.StartDate;
            education.EndDate = updated.IsCurrent ? "" : updated.EndDate;
            education.IsCurrent = updated.IsCurrent;
            education.Description = updated.Description;
            education.Grade = updated.Grade;
            education.Visible = updated.Visible;
            education.DisplayOrder = updated.DisplayOrder;

            await _context.SaveChangesAsync();

            return Ok(education);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var education = await _context.Educations.FindAsync(id);

            if (education == null)
                return NotFound();

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Education deleted successfully." });
        }
    }
}
