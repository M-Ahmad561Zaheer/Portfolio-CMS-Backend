namespace PortfolioBackend.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Slug { get; set; } = "";

        public string Content { get; set; } = "";
        public string Excerpt { get; set; } = "";
        public string Category { get; set; } = "";
        public int ReadingTime { get; set; }
        public bool IsPublished { get; set; } = true;
        public bool Featured { get; set; }
        public DateTime? PublishedAt { get; set; }

        public string Thumbnail { get; set; } = "";

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
