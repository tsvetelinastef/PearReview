using Microsoft.AspNetCore.Identity;

namespace PearReview.Areas.Identity.Data
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }
    }
}
