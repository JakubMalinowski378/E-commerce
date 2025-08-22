using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetUserProductsAsync(Guid userId);
    Task<(IEnumerable<Product>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber);
    Task<Guid> CreateAsync(Product product);
    Task DeleteAsync(Guid productId);
    Task DeleteUserProductsAsync(Guid userId);
}
