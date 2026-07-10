using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Data;
using PortfolioBackend.Models;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            return Ok(new
            {
                projects = _context.Projects.Count(),
                messages = _context.ContactMessages.Count(),
                admins = _context.AdminUsers.Count(),
                blogs = _context.Blogs.Count(),
                testimonials = _context.Testimonials.Count(),
                experience = _context.Experiences.Count(),
                skills = _context.Skills.Count(),
                services = _context.ServiceItems.Count(),
            });
        }
    }
}