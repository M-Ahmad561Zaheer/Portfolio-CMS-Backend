using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioBackend.Data;
using PortfolioBackend.Models;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            // Optimized: Added .AsNoTracking() for blazing fast load times
            var services = await _context.ServiceItems
                .AsNoTracking()
                .OrderBy(s => s.DisplayOrder)
                .ToListAsync();

            return Ok(services);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateService(ServiceItem service)
        {
            _context.ServiceItems.Add(service);
            await _context.SaveChangesAsync();

            return Ok(service);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateService(int id, ServiceItem updatedService)
        {
            // FindAsync is correct here as we need to track and update the record
            var service = await _context.ServiceItems.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            service.Title = updatedService.Title;
            service.Description = updatedService.Description;
            service.IconName = updatedService.IconName;
            service.DisplayOrder = updatedService.DisplayOrder;

            await _context.SaveChangesAsync();

            return Ok(service);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.ServiceItems.FindAsync(id);

            if (service == null)
                return NotFound();

            _context.ServiceItems.Remove(service);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Service deleted successfully." });
        }
    }
}