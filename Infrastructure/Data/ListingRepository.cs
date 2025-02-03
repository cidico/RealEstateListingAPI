using RealEstateListingApi.Core.Entities;
using RealEstateListingApi.Core.Interfaces;

namespace RealEstateListingApi.Infrastructure.Data;

public class ListingRepository : GenericRepository<Listing>, IListingRepository
{
    public ListingRepository(ApplicationDbContext context) : base(context)
    {
    }
}