namespace PortfolioBackend.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public string Message { get; set; } = "";

        public bool IsReplied { get; set; } = false;

        public string ReplyMessage { get; set; } = "";

        public DateTime? RepliedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}