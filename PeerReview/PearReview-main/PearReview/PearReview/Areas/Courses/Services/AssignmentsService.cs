using System;
using Microsoft.EntityFrameworkCore;

using PearReview.Areas.Courses.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PearReview.Data;

namespace PearReview.Areas.Courses.Services
{
    public class AssignmentsService
    {
        private readonly AppDbContext _context;

        public AssignmentsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PearReview.Areas.Courses.Models.Assignment>> GetAssignmentsAsync(string? courseId)
        {
            if (courseId == null) return new List<PearReview.Areas.Courses.Models.Assignment>();
            
                int courseIdInt = int.Parse(courseId);
                return await _context.Assignments
               .Where(a => a.CourseId == courseIdInt)
               .ToListAsync();
            
        }


        public async Task<Assignment> GetAssignmentAsync(int assignmentId)
        {
            var assignment = await _context.Assignments.FindAsync(assignmentId);
            if (assignment == null) throw new Exception("Assignment not found");
            return assignment;
        }


        public async Task CreateAssignmentAsync(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssignmentAsync(Assignment assignment)
        {
            _context.Assignments.Update(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAssignmentAsync(Assignment assignment)
        {
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
        }
    }
}
