using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;
public class RatingRepository(EcommerceDbContext context) : IRatingRepository
{
    private readonly EcommerceDbContext _context = context;
    public async Task CreateRating(Rating rating)
    {
        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRating(Rating rating)
    {
        _context.Ratings.Remove(rating);
        await _context.SaveChangesAsync();
    }

    public async Task<Rating?> GetRatingById(Guid id)
        => await _context.Ratings
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Rating?> GetRatingByUserIdAndProductId(Guid userId, Guid ProductId)
        => await _context.Ratings
        .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == ProductId);

    public async Task<IEnumerable<Rating>> GetRatings()
        => await _context.Ratings.ToListAsync();

    public Task SaveChanges()
        => _context.SaveChangesAsync();
}
