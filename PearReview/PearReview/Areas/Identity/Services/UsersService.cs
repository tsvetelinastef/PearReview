using Microsoft.EntityFrameworkCore;
using PearReview.Areas.Identity.Data;
using PearReview.Data;

namespace PearReview.Areas.Identity.Services
{
    public class UsersService
    {
        private readonly DataContext _context;

        public UsersService(DataContext context)
        {
            _context = context;
        }

        public Task<List<AppUser>> GetUsersAsync()
        {
            if (_context.Users == null)
            {
                return Task.FromResult(new List<AppUser>());
            }
            return _context.Users.ToListAsync();
        }
    }
}
