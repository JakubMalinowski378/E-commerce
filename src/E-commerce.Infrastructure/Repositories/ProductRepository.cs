using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;

public class ProductRepository(
    ECommerceDbContext dbContext)
    : IProductRepository
{
    public async Task<Guid> CreateAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);
        return product.Id;
    }

    public async Task DeleteAsync(Guid productId)
        => await dbContext.Products
            .Where(p => p.Id == productId)
            .ExecuteDeleteAsync();

    public async Task DeleteUserProductsAsync(Guid userId)
    {
        await dbContext.Products
             .Where(p => p.UserId == userId)
             .ExecuteDeleteAsync();
    }

    public async Task<(IEnumerable<Product>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber)
    {
        var query = dbContext.Products.AsQueryable();

        if (!string.IsNullOrEmpty(searchPhrase))
        {
            query = query.Where(p => p.Name.Contains(searchPhrase, StringComparison.InvariantCultureIgnoreCase));
        }

        int totalCount = await query.CountAsync();
        var products = await query
            .OrderBy(p => p.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (products, totalCount);
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
        => await dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Product>> GetUserProductsAsync(Guid userId)
        => await dbContext.Products
            .Where(p => p.UserId == userId)
            .ToListAsync();
}
