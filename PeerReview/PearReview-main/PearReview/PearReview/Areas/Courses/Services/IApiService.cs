using PearReview.Areas.Courses.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PearReview.Areas.Courses.Services
{
    public interface IApiService
    {
        Task<ApiResponse> GetAssignmentsAsync(string courseId);
        Task<ApiResponse> SaveReviewCriteriaAsync(string courseId, ReviewCriteria reviewCriteria);
        Task<ApiResponse> AddAssignmentForCourse(string courseId, Assignment assignment);
        Task<IEnumerable<Assignment>> GetAssignmentsForCourse(string courseId);
    }
}
