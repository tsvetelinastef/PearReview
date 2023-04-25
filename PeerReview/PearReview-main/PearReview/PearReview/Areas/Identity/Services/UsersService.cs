using Microsoft.EntityFrameworkCore;
using PearReview.Areas.Courses.Data;
using PearReview.Areas.Identity.Data;
using PearReview.Data;

namespace PearReview.Areas.Identity.Services
{
    public class UsersService
    {
        private readonly IDbContextFactory<AppDbContext> factory;

        public UsersService(IDbContextFactory<AppDbContext> contextFactory)
        {
            factory = contextFactory;
        }

        public Task<List<AppUser>> GetUsersAsync()
        {
            using AppDbContext context = factory.CreateDbContext();

            if (context.Users == null)
            {
                return Task.FromResult(new List<AppUser>());
            }
            return context.Users.ToListAsync();
        }
    }
}
