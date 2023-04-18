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

        public async Task<List<Course>> GetCoursesAsync()
        {
            if (_context.Courses == null)
            {
                return new List<Course>();
            }
            return await _context.Courses.ToListAsync();
        }

        public async Task<List<Course>> GetCoursesWithUsersAsync()
        {
            if (_context.Courses == null)
            {
                return new List<Course>();
            }
            return await _context.Courses
                .Include(c => c.Teacher)
                .ToListAsync();
        }

        public async Task<int> AddCourse(Course course)
        {
            _context.Courses.Add(course);
            return await _context.SaveChangesAsync();
        }
    }
}
