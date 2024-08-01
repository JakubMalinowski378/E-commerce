using E_commerce.Domain.Entities;
using System.Linq.Expressions;

namespace E_commerce.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid id,
        params Expression<Func<Product, object>>[] includePredicates);
    Task<IEnumerable<Product>> GetUserProducts(Guid userId,
        params Expression<Func<Product, object>>[] includePredicates);
    Task<(IEnumerable<Product>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber,
        params Expression<Func<Product, object>>[] includePredicates);
    Task<Guid> Create(Product product);
    Task Delete(Product product);
    Task SaveChanges();
}
