using Microsoft.EntityFrameworkCore.Storage;
using RealEstateListingApi.Core.Interfaces;

namespace RealEstateListingApi.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IListingRepository _listingRepository;
    private bool _disposed;
    private IDbContextTransaction _currentTransaction;

    public UnitOfWork(ApplicationDbContext context, IListingRepository listingRepository)
    {
        _context = context;
        _listingRepository = listingRepository;
    }
    
    public IDbContextTransaction GetTransaction() => _currentTransaction;
    
    public IListingRepository Listings => _listingRepository;

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null) 
            return;
        
        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_currentTransaction is not null)
        {
            await _currentTransaction.CommitAsync();
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction is not null)
        {
            await _currentTransaction.RollbackAsync();
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}