using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PearReview.Areas.Courses.Data;
using PearReview.Areas.Identity.Data;
using PearReview.Areas.Courses.Models;

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

            // Set Identity table names
            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles");

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "902f24fa-879f-4263-875d-6f7599665c75", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "11931df8-9549-4bd8-87db-63e4b943de08" },
                new IdentityRole { Id = "233e6dbb-5925-4c23-a86c-18d501c53cde", Name = "Teacher", NormalizedName = "TEACHER", ConcurrencyStamp = "234d8db3-b25d-4b6e-bdec-96b944ae87f8" },
                new IdentityRole { Id = "4c5d0099-7b3a-44da-b6e8-d88959814aea", Name = "Student", NormalizedName = "STUDENT", ConcurrencyStamp = "a395e986-8ec5-44d0-80dd-2728bf7ec962" }
            );
        }

        public DbSet<Course> Courses { get; set; }

        public new DbSet<AppUser> Users { get; set; }

        public DbSet<Assignment> Assignments { get; set; }


    }
}
