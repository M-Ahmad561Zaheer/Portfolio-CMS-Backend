namespace PortfolioBackend.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public string LongDescription { get; set; } = "";

        public string ImageUrl { get; set; } = "";

        public string GithubUrl { get; set; } = "";

        public string LiveUrl { get; set; } = "";

        public string TechStack { get; set; } = "";

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}