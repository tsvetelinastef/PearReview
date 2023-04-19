using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PearReview.Areas.Courses.Data;
using PearReview.Areas.Identity.Data;

namespace PearReview.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Course>()
                .HasOne<AppUser>("Teacher") // Course has one User - navigation property name = "Teacher"
                .WithMany() // User has many Courses - no navigational property
                .IsRequired();
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<AppUser> Users { get; set; }
    }
}
