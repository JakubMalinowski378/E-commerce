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
        //var user = await _context.Users.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.Id == rating.UserId);
        //user.Ratings.Add(rating);
        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRating(Rating rating)
    {

        _context.Ratings.Remove(rating);
        await _context.SaveChangesAsync();
    }

    public async Task<Rating> GetRatingById(Guid id)
    {
        return await _context.Ratings.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Rating>> GetRatings()
    => await _context.Ratings.ToListAsync();

    public async Task<IEnumerable<Rating>> GetRatingsByProductId(Guid id)
    => await _context.Ratings.Where(x => x.ProductId == id).ToListAsync();

    public async Task<IEnumerable<Rating>> GetRatingsByUserId(Guid id)
    => await _context.Ratings.Where(x => x.UserId == id).ToListAsync();


    public async Task UppdateRating(Rating rating)
    {
        _context.Ratings.Update(rating);
        await _context.SaveChangesAsync();
    }
}
