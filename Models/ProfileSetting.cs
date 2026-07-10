namespace PortfolioBackend.Models
{
    public class ProfileSetting
    {
        public int Id { get; set; }

        public string FullName { get; set; } = "";
        public string Role { get; set; } = "";
        public string ShortBio { get; set; } = "";
        public string About { get; set; } = "";

        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Location { get; set; } = "";

        public string GithubUrl { get; set; } = "";
        public string LinkedinUrl { get; set; } = "";
        public string ResumeUrl { get; set; } = "";

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}