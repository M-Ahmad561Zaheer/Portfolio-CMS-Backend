using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioBackend.Data;
using PortfolioBackend.Models;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestimonialsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestimonialsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTestimonials()
        {
            return Ok(await _context.Testimonials
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestimonial(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);

            if (testimonial == null)
                return NotFound();

            return Ok(testimonial);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Testimonial testimonial)
        {
            _context.Testimonials.Add(testimonial);

            await _context.SaveChangesAsync();

            return Ok(testimonial);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            Testimonial updated)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);

            if (testimonial == null)
                return NotFound();

            testimonial.ClientName = updated.ClientName;
            testimonial.Company = updated.Company;
            testimonial.Position = updated.Position;
            testimonial.Review = updated.Review;
            testimonial.Rating = updated.Rating;

            await _context.SaveChangesAsync();

            return Ok(testimonial);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);

            if (testimonial == null)
                return NotFound();

            _context.Testimonials.Remove(testimonial);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}