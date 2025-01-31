using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetUserProducts(Guid userId);
    Task<(IEnumerable<Product>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber);
    Task<Guid> Create(Product product);
    Task Delete(Product product);
    Task Update(Product product);
    Task DeleteUserProducts(Guid userId);
}
