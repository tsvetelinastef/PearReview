using Microsoft.EntityFrameworkCore;
using PearReview.Areas.Courses.Data;
using PearReview.Areas.Identity.Data;
using PearReview.Data;

namespace PearReview.Areas.Courses.Services
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

        public Task<List<Course>> GetCoursesWithUsersAsync()
        {
            if (_context.Courses == null)
            {
                return Task.FromResult(new List<Course>());
            }
            return _context.Courses
                .Include(c => c.Teacher)
                .ToListAsync();
        }

        public Task AddCourse(Course course)
        {
            _context.Courses.Add(course);
            return _context.SaveChangesAsync();
        }
    }
}
