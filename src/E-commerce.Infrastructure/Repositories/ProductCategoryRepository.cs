using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;

namespace E_commerce.Infrastructure.Repositories;
public class ProductCategoryRepository(EcommerceDbContext dbContext) : IProductCategoryRepository
{
    private readonly EcommerceDbContext _dbcontext = dbContext;

    public async Task AddCategoriesToProduct(IEnumerable<ProductCategory> productCategories)
    {
        await _dbcontext.ProductCategories.AddRangeAsync(productCategories);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task AddCategoryToProduct(ProductCategory productCategory)
    {
        await _dbcontext.ProductCategories.AddAsync(productCategory);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task RemoveCategoriesFromProduct(IEnumerable<ProductCategory> productCategories)
    {
        _dbcontext.ProductCategories.RemoveRange(productCategories);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task RemoveCategoryFromProduct(ProductCategory productCategory)
    {
        _dbcontext.ProductCategories.Remove(productCategory);
        await _dbcontext.SaveChangesAsync();
    }
}
