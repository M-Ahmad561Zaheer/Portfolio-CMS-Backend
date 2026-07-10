namespace PortfolioBackend.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Slug { get; set; } = "";

        public string Content { get; set; } = "";

        public string Thumbnail { get; set; } = "";

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}