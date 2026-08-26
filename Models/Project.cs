namespace PortfolioBackend.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string Slug { get; set; } = "";

        public string Description { get; set; } = "";

        public string LongDescription { get; set; } = "";

        public string ImageUrl { get; set; } = "";

        public string GithubUrl { get; set; } = "";

        public string LiveUrl { get; set; } = "";

        public string TechStack { get; set; } = "";
        public string Screenshots { get; set; } = "";
        public bool Featured { get; set; }
        public int DisplayOrder { get; set; } = 1;
        public bool Visible { get; set; } = true;
        public string Status { get; set; } = "Completed";
        public string Problem { get; set; } = "";
        public string Solution { get; set; } = "";
        public string TechnicalApproach { get; set; } = "";
        public string KeyFeatures { get; set; } = "";
        public string Challenges { get; set; } = "";
        public string LessonsLearned { get; set; } = "";

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
