using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PearReview.Areas.Courses.Models;

namespace PearReview.Areas.Courses.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse> GetAssignmentsAsync(string courseId)
        {
            var response = await _httpClient.GetAsync($"/api/courses/{courseId}/assignments");
            var content = await response.Content.ReadAsStringAsync();
            var assignments = JsonConvert.DeserializeObject<Assignment[]>(content);
            return new ApiResponse(assignments);
        }

        public async Task<ApiResponse> SaveReviewCriteriaAsync(string courseId, ReviewCriteria reviewCriteria)
        {
            var json = JsonConvert.SerializeObject(reviewCriteria);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/courses/{courseId}/reviewcriteria", content);
            return new ApiResponse();
        }
    }
}
