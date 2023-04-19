using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PearReview.Data;

namespace PearReview.Areas.Identity.Data
{
    public class AppUserStore : UserOnlyStore<AppUser, AppDbContext, string>
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public AppUserStore(IDbContextFactory<AppDbContext> dbContextFactory)
            : base(dbContextFactory.CreateDbContext())
        {
            _dbContextFactory = dbContextFactory;
        }

        public override AppDbContext Context
        {
            get { return _dbContextFactory.CreateDbContext(); }
        }
    }
}
