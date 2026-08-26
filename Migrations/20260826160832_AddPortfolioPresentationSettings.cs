using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioBackend.Migrations;

public partial class AddPortfolioPresentationSettings : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        AddBool(migrationBuilder, "AboutEnabled", true);
        AddBool(migrationBuilder, "SkillsEnabled", true);
        AddBool(migrationBuilder, "ProjectsEnabled", true);
        AddBool(migrationBuilder, "ExperienceEnabled", true);
        AddBool(migrationBuilder, "ArchitectureEnabled", true);
        AddBool(migrationBuilder, "GithubEnabled", true);
        AddBool(migrationBuilder, "BlogEnabled", true);
        AddBool(migrationBuilder, "ContactEnabled", true);
        AddBool(migrationBuilder, "ContactFormEnabled", true);
        AddBool(migrationBuilder, "MusicEnabled", false);
        AddText(migrationBuilder, "MusicHeading", "While You Browse");
        AddText(migrationBuilder, "MusicDescription", "Lo-fi / Coding Vibes");
        AddText(migrationBuilder, "MusicLabel", "Focus Mode");
        AddText(migrationBuilder, "MusicUrl", "");
        AddText(migrationBuilder, "MusicCoverUrl", "");
        AddText(migrationBuilder, "ArchitectureTitle", "System Design & Architecture");
        AddText(migrationBuilder, "ArchitectureDescription", "Designing scalable and maintainable applications.");
        AddText(migrationBuilder, "ContactTitle", "Let's Build Something Great");
        AddText(migrationBuilder, "ContactSubtitle", "Have a project, opportunity or idea? I'd be glad to hear about it.");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        foreach (var column in new[] { "AboutEnabled", "SkillsEnabled", "ProjectsEnabled", "ExperienceEnabled", "ArchitectureEnabled", "GithubEnabled", "BlogEnabled", "ContactEnabled", "ContactFormEnabled", "MusicEnabled", "MusicHeading", "MusicDescription", "MusicLabel", "MusicUrl", "MusicCoverUrl", "ArchitectureTitle", "ArchitectureDescription", "ContactTitle", "ContactSubtitle" })
            migrationBuilder.DropColumn(column, "ProfileSettings");
    }

    private static void AddBool(MigrationBuilder migrationBuilder, string name, bool value) =>
        migrationBuilder.AddColumn<bool>(name, "ProfileSettings", "boolean", nullable: false, defaultValue: value);

    private static void AddText(MigrationBuilder migrationBuilder, string name, string value) =>
        migrationBuilder.AddColumn<string>(name, "ProfileSettings", "text", nullable: false, defaultValue: value);
}
