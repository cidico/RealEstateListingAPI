namespace RealEstateListingApi.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IListingRepository Listings { get; }
    
    Task<int> SaveChangesAsync();
    
    Task BeginTransactionAsync();
    
    Task CommitTransactionAsync();
    
    Task RollbackTransactionAsync();
}