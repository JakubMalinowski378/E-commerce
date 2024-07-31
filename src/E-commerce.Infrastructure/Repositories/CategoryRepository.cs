using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;
public class CategoryRepository(EcommerceDbContext dbContext)
    : ICategoryRepository
{
    private readonly EcommerceDbContext _dbContext = dbContext;

    public async Task Create(Category category)
    {
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Category category)
    {
        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await _dbContext.Categories.ToListAsync();
}
