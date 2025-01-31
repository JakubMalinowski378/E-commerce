using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;
public class ProductCategoryRepository(EcommerceDbContext dbContext) : IProductCategoryRepository
{
    private readonly EcommerceDbContext _dbContext = dbContext;

    public async Task AddCategoriesToProduct(IEnumerable<ProductCategory> productCategories)
    {
        await _dbContext.ProductCategories.AddRangeAsync(productCategories);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddCategoryToProduct(ProductCategory productCategory)
    {
        await _dbContext.ProductCategories.AddAsync(productCategory);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductCategory>> GetProductCategories(Guid productId)
        => await _dbContext.ProductCategories
                            .Where(x => x.ProductId == productId)
                            .Include(x => x.Category)
                            .ToListAsync();

    public async Task RemoveCategoriesFromProduct(IEnumerable<ProductCategory> productCategories)
    {
        _dbContext.ProductCategories.RemoveRange(productCategories);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveCategoryFromProduct(ProductCategory productCategory)
    {
        _dbContext.ProductCategories.Remove(productCategory);
        await _dbContext.SaveChangesAsync();
    }

    public Task SaveChangesAsync()
        => _dbContext.SaveChangesAsync();
}
