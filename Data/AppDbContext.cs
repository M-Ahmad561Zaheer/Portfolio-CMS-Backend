using Microsoft.EntityFrameworkCore;
using PortfolioBackend.Models;

namespace PortfolioBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options
        ) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<AdminUser> AdminUsers { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Resume> Resumes { get; set; }

        public DbSet<ProfileSetting> ProfileSettings { get; set; }

        public DbSet<ServiceItem> ServiceItems { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Testimonial> Testimonials { get; set; }

        // Fixed: Small 'experiences' duplicate line removed, standard capital PascalCase retained
        public DbSet<Experience> Experiences { get; set; }
    }
}