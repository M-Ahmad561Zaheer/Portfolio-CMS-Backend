using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioBackend.Data;
using PortfolioBackend.Models;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            // Yeh bilkul perfect hai! OrderByDescending aur AsNoTracking dono behtareen hain.
            return Ok(await _context.Blogs
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            // Fast Read-only fetch using AsNoTracking
            var blog = await _context.Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (blog == null)
                return NotFound();

            return Ok(blog);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return Ok(blog);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, Blog updatedBlog)
        {
            // Yahan AsNoTracking nahi lagana kyunki humne isko edit kar k save karna hai, so FindAsync is fine here!
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
                return NotFound();

            blog.Title = updatedBlog.Title;
            blog.Slug = updatedBlog.Slug;
            blog.Content = updatedBlog.Content;
            blog.Thumbnail = updatedBlog.Thumbnail;

            await _context.SaveChangesAsync();

            return Ok(blog);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
                return NotFound();

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}