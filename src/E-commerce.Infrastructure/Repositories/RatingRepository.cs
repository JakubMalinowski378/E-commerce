using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;

public class RatingRepository(
    ECommerceDbContext dbContext)
    : Repository<Rating>(dbContext), IRatingRepository
{
    public async Task<bool> HasUserRatedProductAsync(Guid userId, Guid productId)
        => await _dbSet.AnyAsync(r => r.UserId == userId && r.ProductId == productId);
}
