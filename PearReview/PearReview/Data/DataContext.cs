using Microsoft.EntityFrameworkCore;

namespace PearReview.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

    }
}
