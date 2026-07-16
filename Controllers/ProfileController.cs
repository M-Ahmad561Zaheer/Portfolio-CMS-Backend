using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioBackend.Data;
using PortfolioBackend.Models;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            // Optimized: Added .AsNoTracking() to load profile info instantly
            var profile = await _context.ProfileSettings.AsNoTracking().FirstOrDefaultAsync();

            if (profile == null)
            {
                profile = new ProfileSetting
                {
                    FullName = "Ahmad Zaheer",
                    Role = ".NET & React Developer",
                    ShortBio = "I build scalable web applications, APIs, dashboards, and portfolio CMS systems.",
                    About = "I am a full-stack developer focused on React, Tailwind CSS, ASP.NET Core, and database-driven web applications.",
                    Email = "your-email@example.com",
                    Phone = "+92 300 0000000",
                    Location = "Pakistan",
                    GithubUrl = "#",
                    LinkedinUrl = "#",
                    ResumeUrl = "#",
                    UpdatedAt = DateTime.UtcNow
                };

                // Add call tracking manage kar le gi, so AsNoTracking query par koi asar nahi paray ga tab
                _context.ProfileSettings.Add(profile);
                await _context.SaveChangesAsync();
            }

            return Ok(profile);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(ProfileSetting updated)
        {
            // No AsNoTracking here because we need to modify this existing record!
            var profile = await _context.ProfileSettings.FirstOrDefaultAsync();

            if (profile == null)
            {
                updated.UpdatedAt = DateTime.UtcNow;
                _context.ProfileSettings.Add(updated);
                await _context.SaveChangesAsync();
                return Ok(updated);
            }

            profile.FullName = updated.FullName;
            profile.Role = updated.Role;
            profile.ShortBio = updated.ShortBio;
            profile.About = updated.About;
            profile.Email = updated.Email;
            profile.Phone = updated.Phone;
            profile.Location = updated.Location;
            profile.GithubUrl = updated.GithubUrl;
            profile.LinkedinUrl = updated.LinkedinUrl;
            profile.ResumeUrl = updated.ResumeUrl;
            profile.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(profile);
        }
    }
}