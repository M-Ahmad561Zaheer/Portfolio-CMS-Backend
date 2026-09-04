using System.ComponentModel.DataAnnotations;

namespace PortfolioBackend.Models
{
    public class Education
    {
        public int Id { get; set; }

        [Required]
        public string Degree { get; set; } = "";

        [Required]
        public string Institution { get; set; } = "";
        public string Location { get; set; } = "";

        public string StartDate { get; set; } = "";

        public string EndDate { get; set; } = "";
        public bool IsCurrent { get; set; }

        public string Description { get; set; } = "";
        public string Grade { get; set; } = "";
        public bool Visible { get; set; } = true;

        public int DisplayOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
