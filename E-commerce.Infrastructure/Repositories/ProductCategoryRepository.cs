using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;
public class ProductCategoryRepository(EcommerceDbContext dbContext)
    : IProductCategoryRepository
{
    private readonly EcommerceDbContext _dbContext = dbContext;

    public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        => await _dbContext.ProductCategories.ToListAsync();
}
