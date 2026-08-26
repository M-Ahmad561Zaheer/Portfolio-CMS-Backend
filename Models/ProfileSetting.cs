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

        public bool AboutEnabled { get; set; } = true;
        public bool SkillsEnabled { get; set; } = true;
        public bool ProjectsEnabled { get; set; } = true;
        public bool ExperienceEnabled { get; set; } = true;
        public bool ArchitectureEnabled { get; set; } = true;
        public bool GithubEnabled { get; set; } = true;
        public bool BlogEnabled { get; set; } = true;
        public bool ContactEnabled { get; set; } = true;
        public bool ContactFormEnabled { get; set; } = true;
        public bool MusicEnabled { get; set; }
        public string MusicHeading { get; set; } = "While You Browse";
        public string MusicDescription { get; set; } = "Lo-fi / Coding Vibes";
        public string MusicLabel { get; set; } = "Focus Mode";
        public string MusicUrl { get; set; } = "";
        public string MusicCoverUrl { get; set; } = "";
        public string ArchitectureTitle { get; set; } = "System Design & Architecture";
        public string ArchitectureDescription { get; set; } = "Designing scalable and maintainable applications.";
        public string ContactTitle { get; set; } = "Let's Build Something Great";
        public string ContactSubtitle { get; set; } = "Have a project, opportunity or idea? I'd be glad to hear about it.";
        public string AvailabilityText { get; set; } = "";
        public bool ExploringEnabled { get; set; } = true;
        public string ExploringItems { get; set; } = "";
        public string ExploringStatus { get; set; } = "Learning";

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
