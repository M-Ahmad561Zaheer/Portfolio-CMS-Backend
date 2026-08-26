using System.ComponentModel.DataAnnotations;

namespace PortfolioBackend.DTOs
{
    public class ContactMessageDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(3000, MinimumLength = 10)]
        public string Message { get; set; } = string.Empty;
    }
}
