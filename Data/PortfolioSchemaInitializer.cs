using Microsoft.EntityFrameworkCore;

namespace PortfolioBackend.Data;

public static class PortfolioSchemaInitializer
{
    public static async Task InitializeAsync(AppDbContext dbContext)
    {
        // A brand-new database receives the complete current EF model.
        // Existing Render databases are upgraded below without dropping data and
        // without depending on legacy migration-history records being present.
        await dbContext.Database.EnsureCreatedAsync();

        foreach (var statement in Statements)
        {
            await dbContext.Database.ExecuteSqlRawAsync(statement);
        }
    }

    private static readonly string[] Statements =
    [
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "AboutEnabled" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "SkillsEnabled" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ProjectsEnabled" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ExperienceEnabled" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ArchitectureEnabled" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "GithubEnabled" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "BlogEnabled" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ContactEnabled" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ContactFormEnabled" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "TestimonialsEnabled" boolean NOT NULL DEFAULT FALSE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "MusicEnabled" boolean NOT NULL DEFAULT FALSE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "MusicHeading" text NOT NULL DEFAULT 'While You Browse';""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "MusicDescription" text NOT NULL DEFAULT 'Lo-fi / Coding Vibes';""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "MusicLabel" text NOT NULL DEFAULT 'Focus Mode';""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "MusicUrl" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "MusicCoverUrl" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ArchitectureTitle" text NOT NULL DEFAULT 'System Design & Architecture';""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ArchitectureDescription" text NOT NULL DEFAULT 'Designing scalable and maintainable applications.';""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ContactTitle" text NOT NULL DEFAULT 'Let''s Build Something Great';""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ContactSubtitle" text NOT NULL DEFAULT 'Have a project, opportunity or idea? I''d be glad to hear about it.';""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "AvailabilityText" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ExploringEnabled" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ExploringItems" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "ProfileSettings" ADD COLUMN IF NOT EXISTS "ExploringStatus" text NOT NULL DEFAULT 'Learning';""",

        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "Slug" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "Screenshots" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "Featured" boolean NOT NULL DEFAULT FALSE;""",
        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "DisplayOrder" integer NOT NULL DEFAULT 1;""",
        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "Visible" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "Status" text NOT NULL DEFAULT 'Completed';""",
        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "Problem" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "Solution" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "TechnicalApproach" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "KeyFeatures" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "Challenges" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Projects" ADD COLUMN IF NOT EXISTS "LessonsLearned" text NOT NULL DEFAULT '';""",

        """ALTER TABLE "Skills" ADD COLUMN IF NOT EXISTS "IconUrl" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Skills" ADD COLUMN IF NOT EXISTS "Proficiency" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Skills" ADD COLUMN IF NOT EXISTS "Note" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Skills" ADD COLUMN IF NOT EXISTS "Visible" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "Skills" ADD COLUMN IF NOT EXISTS "Featured" boolean NOT NULL DEFAULT FALSE;""",

        """ALTER TABLE "Experience" ADD COLUMN IF NOT EXISTS "EmploymentType" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Experience" ADD COLUMN IF NOT EXISTS "Location" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Experience" ADD COLUMN IF NOT EXISTS "Technologies" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Experience" ADD COLUMN IF NOT EXISTS "IsCurrent" boolean NOT NULL DEFAULT FALSE;""",
        """ALTER TABLE "Experience" ADD COLUMN IF NOT EXISTS "Visible" boolean NOT NULL DEFAULT TRUE;""",

        """ALTER TABLE "Blogs" ADD COLUMN IF NOT EXISTS "Excerpt" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Blogs" ADD COLUMN IF NOT EXISTS "Category" text NOT NULL DEFAULT '';""",
        """ALTER TABLE "Blogs" ADD COLUMN IF NOT EXISTS "ReadingTime" integer NOT NULL DEFAULT 0;""",
        """ALTER TABLE "Blogs" ADD COLUMN IF NOT EXISTS "IsPublished" boolean NOT NULL DEFAULT TRUE;""",
        """ALTER TABLE "Blogs" ADD COLUMN IF NOT EXISTS "Featured" boolean NOT NULL DEFAULT FALSE;""",
        """ALTER TABLE "Blogs" ADD COLUMN IF NOT EXISTS "PublishedAt" timestamp with time zone NULL;"""
    ];
}
