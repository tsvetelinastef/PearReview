using Microsoft.EntityFrameworkCore;

namespace PearReview.Data
{
    public class CoursesService
    {
        private readonly DataContext _context;

        public CoursesService(DataContext context)
        {
            _context = context;
        }

        public Task<List<Course>> GetCoursesAsync()
        {
            if (_context.Courses == null)
            {
                return Task.FromResult(new List<Course>());
            }
            return _context.Courses.ToListAsync();
        }

        public Task AddCourse(Course course)
        {
            _context.Courses.Add(course);
            return _context.SaveChangesAsync();
        }
    }
}
