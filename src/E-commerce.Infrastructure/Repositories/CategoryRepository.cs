using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;
public class CategoryRepository(EcommerceDbContext dbContext)
    : ICategoryRepository
{
    private readonly EcommerceDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await _dbContext.Categories.ToListAsync();
}
