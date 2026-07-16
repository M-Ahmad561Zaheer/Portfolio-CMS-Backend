using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioBackend.Data;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetStats()
        {
            // Ab hum Task.WhenAll use nahi kar rahe taake code beginner-friendly rahe,
            // lekin isay async (CountAsync) kar diya hai jo threads ko block nahi hone dega.
            var projectsCount = await _context.Projects.CountAsync();
            var messagesCount = await _context.ContactMessages.CountAsync();
            var adminsCount = await _context.AdminUsers.CountAsync();
            var blogsCount = await _context.Blogs.CountAsync();
            var testimonialsCount = await _context.Testimonials.CountAsync();
            var experiencesCount = await _context.Experiences.CountAsync();
            var skillsCount = await _context.Skills.CountAsync();
            var servicesCount = await _context.ServiceItems.CountAsync();

            return Ok(new
            {
                projects = projectsCount,
                messages = messagesCount,
                admins = adminsCount,
                blogs = blogsCount,
                testimonials = testimonialsCount,
                experience = experiencesCount,
                skills = skillsCount,
                services = servicesCount
            });
        }
    }
}