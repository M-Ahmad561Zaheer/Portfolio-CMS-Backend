namespace PortfolioBackend.Models
{
    public class Skill
    {
        public int Id { get; set; }

        public string Category { get; set; } = "";

        public string Name { get; set; } = "";

        public int DisplayOrder { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}