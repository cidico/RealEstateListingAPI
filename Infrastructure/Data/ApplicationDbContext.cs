using Microsoft.EntityFrameworkCore;
using RealEstateListingApi.Core.Entities;

namespace RealEstateListingApi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Listing> Listings { get; set; }
    }
}
