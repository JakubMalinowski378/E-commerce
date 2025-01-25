using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;
public class RatingRepository(EcommerceDbContext dbContext) : IRatingRepository
{
    private readonly EcommerceDbContext _dbcontext = dbContext;
    public async Task CreateRating(Rating rating)
    {
        _dbcontext.Ratings.Add(rating);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task DeleteRating(Rating rating)
    {
        _dbcontext.Ratings.Remove(rating);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Rating>> GetProductRatings(Guid productId)
        => await _dbcontext.Ratings
            .Where(x => x.ProductId == productId).ToListAsync();

    public async Task<Rating?> GetRatingById(Guid ratingId)
        => await _dbcontext.Ratings
            .FirstOrDefaultAsync(x => x.Id == ratingId);

    public async Task<Rating?> GetRatingByUserIdAndProductId(Guid userId, Guid ProductId)
        => await _dbcontext.Ratings
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == ProductId);

    public async Task<IEnumerable<Rating>> GetRatings()
        => await _dbcontext.Ratings.ToListAsync();

    public Task SaveChanges()
        => _dbcontext.SaveChangesAsync();
}
