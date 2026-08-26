using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PortfolioBackend.Data;

#nullable disable

namespace PortfolioBackend.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260826180000_ExpandPortfolioContent")]
public sealed class ExpandPortfolioContent : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        AddText(migrationBuilder, "Projects", "Slug");
        AddText(migrationBuilder, "Projects", "Screenshots");
        AddBool(migrationBuilder, "Projects", "Featured", false);
        AddInt(migrationBuilder, "Projects", "DisplayOrder", 1);
        AddBool(migrationBuilder, "Projects", "Visible", true);
        AddText(migrationBuilder, "Projects", "Status", "Completed");
        foreach (var name in new[] { "Problem", "Solution", "TechnicalApproach", "KeyFeatures", "Challenges", "LessonsLearned" })
            AddText(migrationBuilder, "Projects", name);

        foreach (var name in new[] { "IconUrl", "Proficiency", "Note" })
            AddText(migrationBuilder, "Skills", name);
        AddBool(migrationBuilder, "Skills", "Visible", true);
        AddBool(migrationBuilder, "Skills", "Featured", false);

        foreach (var name in new[] { "EmploymentType", "Location", "Technologies" })
            AddText(migrationBuilder, "Experience", name);
        AddBool(migrationBuilder, "Experience", "IsCurrent", false);
        AddBool(migrationBuilder, "Experience", "Visible", true);

        foreach (var name in new[] { "Excerpt", "Category" })
            AddText(migrationBuilder, "Blogs", name);
        AddInt(migrationBuilder, "Blogs", "ReadingTime", 0);
        AddBool(migrationBuilder, "Blogs", "IsPublished", true);
        AddBool(migrationBuilder, "Blogs", "Featured", false);
        migrationBuilder.AddColumn<DateTime>("PublishedAt", "Blogs", "timestamp with time zone", nullable: true);
        AddBool(migrationBuilder, "ProfileSettings", "TestimonialsEnabled", false);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        Drop(migrationBuilder, "Projects", "Slug", "Screenshots", "Featured", "DisplayOrder", "Visible", "Status", "Problem", "Solution", "TechnicalApproach", "KeyFeatures", "Challenges", "LessonsLearned");
        Drop(migrationBuilder, "Skills", "IconUrl", "Proficiency", "Note", "Visible", "Featured");
        Drop(migrationBuilder, "Experience", "EmploymentType", "Location", "Technologies", "IsCurrent", "Visible");
        Drop(migrationBuilder, "Blogs", "Excerpt", "Category", "ReadingTime", "IsPublished", "Featured", "PublishedAt");
        Drop(migrationBuilder, "ProfileSettings", "TestimonialsEnabled");
    }

    private static void AddText(MigrationBuilder migrationBuilder, string table, string name, string value = "") =>
        migrationBuilder.AddColumn<string>(name, table, "text", nullable: false, defaultValue: value);
    private static void AddBool(MigrationBuilder migrationBuilder, string table, string name, bool value) =>
        migrationBuilder.AddColumn<bool>(name, table, "boolean", nullable: false, defaultValue: value);
    private static void AddInt(MigrationBuilder migrationBuilder, string table, string name, int value) =>
        migrationBuilder.AddColumn<int>(name, table, "integer", nullable: false, defaultValue: value);
    private static void Drop(MigrationBuilder migrationBuilder, string table, params string[] names)
    {
        foreach (var name in names) migrationBuilder.DropColumn(name, table);
    }
}
