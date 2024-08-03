using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;

namespace E_commerce.Infrastructure.Repositories;
public class ProductImageRepository(EcommerceDbContext dbContext) : IProductImageRepository
{
    private readonly EcommerceDbContext _dbContext = dbContext;

    public async Task CreateRange(IEnumerable<ProductImage> productImages)
    {
        await _dbContext.ProductImages.AddRangeAsync(productImages);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(ProductImage productImage)
    {
        _dbContext.ProductImages.Remove(productImage);
        await _dbContext.SaveChangesAsync();
    }
}
