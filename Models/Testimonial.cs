namespace PortfolioBackend.Models
{
    public class Testimonial
    {
        public int Id { get; set; }

        public string ClientName { get; set; } = "";

        public string Company { get; set; } = "";

        public string Position { get; set; } = "";

        public string Review { get; set; } = "";

        public int Rating { get; set; } = 5;

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}