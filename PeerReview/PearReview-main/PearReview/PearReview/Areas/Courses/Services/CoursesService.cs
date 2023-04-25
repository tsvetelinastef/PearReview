using Microsoft.EntityFrameworkCore;
using PearReview.Areas.Courses.Data;
using PearReview.Data;

namespace PearReview.Areas.Courses.Services
{
    public class CoursesService
    {
        private readonly IDbContextFactory<AppDbContext> factory;

        public CoursesService(IDbContextFactory<AppDbContext> contextFactory)
        {
            factory = contextFactory;
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            using AppDbContext context = factory.CreateDbContext();

            if (context.Courses == null)
            {
                return new List<Course>();
            }
            return await context.Courses.ToListAsync();
        }

        public async Task<List<Course>> GetCoursesWithUsersAsync()
        {
            using AppDbContext context = factory.CreateDbContext();

            if (context.Courses == null)
            {
                return new List<Course>();
            }
            return await context.Courses
                .Include(c => c.Teacher)
                .ToListAsync();
        }

        public async Task<int> AddCourse(Course course)
        {
            using AppDbContext context = factory.CreateDbContext();

            context.Courses.Add(course);
            return await context.SaveChangesAsync();
        }

        public async Task<Course?> GetCourseById(int id)
        {
            using AppDbContext context = factory.CreateDbContext();

            return await context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
