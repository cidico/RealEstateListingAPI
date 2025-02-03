namespace RealEstateListingApi.Core.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    
    Task<T> GetByIdAsync(string id);
    
    Task InsertAsync(T entity);
    
    Task DeleteAsync(string id);
}