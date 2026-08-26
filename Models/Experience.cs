namespace PortfolioBackend.Models
{
    public class Experience
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Company { get; set; } = "";
        public string EmploymentType { get; set; } = "";
        public string Location { get; set; } = "";

        public string StartDate { get; set; } = "";

        public string EndDate { get; set; } = "";
        public bool IsCurrent { get; set; }

        public string Description { get; set; } = "";
        public string Technologies { get; set; } = "";
        public bool Visible { get; set; } = true;

        public int DisplayOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
