using System.ComponentModel.DataAnnotations;

namespace PortfolioBackend.DTOs
{
    public class ContactMessageDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;
    }
}