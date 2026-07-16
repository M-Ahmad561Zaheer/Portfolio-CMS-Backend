using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioBackend.Data;
using PortfolioBackend.DTOs;
using PortfolioBackend.Models;
using PortfolioBackend.Services;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

         [HttpPost("register")]
            [ApiExplorerSettings(IgnoreApi = true)]
            public IActionResult Register()
            {
                return NotFound();
            }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.AdminUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            var validPassword = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash
            );

            if (!validPassword)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.Name,
                    user.Email
                }
            });
        }
    }
}