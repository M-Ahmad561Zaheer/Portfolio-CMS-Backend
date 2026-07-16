using Microsoft.AspNetCore.Authorization;
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
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;
        private readonly IConfiguration _config;

        public ContactController(
            AppDbContext context,
            EmailService emailService,
            IConfiguration config
        )
        {
            _context = context; // Fixed: Duplicate line removed!
            _emailService = emailService;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactMessageDto dto)
        {
            var message = new ContactMessage
            {
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message,
                CreatedAt = DateTime.UtcNow
            };

            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync(); 

            var receiverEmail = _config["EmailSettings:ReceiverEmail"];

            var emailBody = $@"
                <h2>New Portfolio Contact Message</h2>
                <p><strong>Name:</strong> {dto.Name}</p>
                <p><strong>Email:</strong> {dto.Email}</p>
                <p><strong>Message:</strong></p>
                <p>{dto.Message}</p>
            ";

            // Fire-and-forget: Sahi chal raha hai aur user lag se bacha rahe ga
            _ = Task.Run(async () =>
            {
                try
                {
                    await _emailService.SendEmailAsync(
                        receiverEmail!,
                        "New Portfolio Contact Message",
                        emailBody,
                        dto.Email
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Background Email sending failed: " + ex.Message);
                }
            });

            return Ok(new { message = "Message sent successfully." });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            // Optimized: Added .AsNoTracking() to speed up database response
            var messages = await _context.ContactMessages
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(messages);
        }

        [Authorize]
        [HttpPost("{id}/reply")]
        public async Task<IActionResult> ReplyToMessage(int id, ReplyMessageDto dto)
        {
            var contactMessage = await _context.ContactMessages.FindAsync(id);

            if (contactMessage == null)
            {
                return NotFound(new { message = "Message not found." });
            }

            contactMessage.IsReplied = true;
            contactMessage.ReplyMessage = dto.ReplyMessage;
            contactMessage.RepliedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var replyBody = $@"
                <p>Hello {contactMessage.Name},</p>
                <p>{dto.ReplyMessage}</p>
                <br/>
                <p>Best regards,</p>
                <p>Ahmad Zaheer</p>
            ";

            _ = Task.Run(async () =>
            {
                try
                {
                    await _emailService.SendEmailAsync(
                        contactMessage.Email,
                        "Reply to your portfolio message",
                        replyBody
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Background Reply Email failed: " + ex.Message);
                }
            });

            return Ok(new { message = "Reply sent successfully." });
        }
    }
}