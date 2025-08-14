using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;

namespace E_commerce.Infrastructure.Repositories;

public class ProductRepository(ECommerceDbContext dbContext) : IProductRepository
{
    public async Task<Guid> Create(Product product)
    {
        await dbContext.Products.AddAsync(product);
        return product.Id;
    }

    public Task Delete(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserProducts(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<Product>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetProductByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetUserProducts(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task Update(Product product)
    {
        throw new NotImplementedException();
    }
}
