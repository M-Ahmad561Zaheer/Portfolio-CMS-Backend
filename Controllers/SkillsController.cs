using Microsoft.EntityFrameworkCore;
using PortfolioBackend.Models;
using PortfolioBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SkillsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SkillsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSkills()
        {
            var skills = await _context.Skills
            .OrderBy(s => s.DisplayOrder)
            .ThenBy(s => s.Category)
            .ToListAsync();

            return Ok(skills);
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return Ok(skill);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateSkill(int id, Skill updatedSkill)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            skill.Category = updatedSkill.Category;
            skill.Name = updatedSkill.Name;
            skill.DisplayOrder = updatedSkill.DisplayOrder;

            await _context.SaveChangesAsync();

            return Ok(skill);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Skill deleted successfully." });
        }

    }
}