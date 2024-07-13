using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;

public class ProductsRepository(EcommerceDbContext dbContext) : IProductsRepository
{
    private readonly EcommerceDbContext _dbContext = dbContext!;

    public async Task<Guid> Create(Product product)
    {
        _dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        return product.Id;
    }
    public async Task<IEnumerable<Product>> GetProductsAsync()
        => await _dbContext.Products.ToListAsync();
    public async Task<Product?> GetProductByIdAsync(Guid id)
        => await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
}
