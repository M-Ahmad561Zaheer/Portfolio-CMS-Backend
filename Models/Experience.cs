namespace PortfolioBackend.Models
{
    public class Experience
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Company { get; set; } = "";

        public string StartDate { get; set; } = "";

        public string EndDate { get; set; } = "";

        public string Description { get; set; } = "";

        public int DisplayOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}