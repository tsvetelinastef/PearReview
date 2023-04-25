using Microsoft.AspNetCore.Identity;

namespace PearReview.Areas.Identity.Data
{
    public class AppUser : IdentityUser
    {
        public UserRole Role { get; set; }

        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }

        [PersonalData]
        public string? Group { get; set; }

        [PersonalData]
        public string? FacultyNumber { get; set; }

    }
}
