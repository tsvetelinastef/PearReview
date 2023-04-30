using Microsoft.EntityFrameworkCore;
using PearReview.Areas.Courses.Data;

namespace PearReview.Data
{
    public class ResourcesService
    {
        private readonly IDbContextFactory<AppDbContext> factory;

        public ResourcesService(IDbContextFactory<AppDbContext> contextFactory)
        {
            factory = contextFactory;
        }

        public async Task<List<Resource>> GetResourcesAsync()
        {
            using AppDbContext context = factory.CreateDbContext();

            if (context.Resources == null)
            {
                return new List<Resource>();
            }
            return await context.Resources.ToListAsync();
        }

        public async Task<int> AddResource(Resource resource)
        {
            using AppDbContext context = factory.CreateDbContext();

            context.Resources.Add(resource);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddResources(ICollection<Resource> resources)
        {
            using AppDbContext context = factory.CreateDbContext();

            foreach (var resource in resources)
            {
                context.Resources.Add(resource);
            }

            return await context.SaveChangesAsync();
        }

    }
}