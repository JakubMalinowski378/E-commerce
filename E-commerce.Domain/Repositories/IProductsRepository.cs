using E_commerce.Domain.Entities;
using System.Linq.Expressions;

namespace E_commerce.Domain.Repositories;

public interface IProductsRepository
{
    Task<Product?> GetProductByIdAsync(Guid id, params Expression<Func<Product, object>>[] includePredicates);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Guid> Create(Product product);
}
