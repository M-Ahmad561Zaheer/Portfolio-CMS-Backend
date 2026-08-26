namespace PortfolioBackend.Models
{
    public class Skill
    {
        public int Id { get; set; }

        public string Category { get; set; } = "";

        public string Name { get; set; } = "";
        public string IconUrl { get; set; } = "";
        public string Proficiency { get; set; } = "";
        public string Note { get; set; } = "";
        public bool Visible { get; set; } = true;
        public bool Featured { get; set; }

        public int DisplayOrder { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
