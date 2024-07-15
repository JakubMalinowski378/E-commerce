using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface IRatingRepository
{
    Task CreateRating(Rating rating);
    Task DeleteRating(Rating rating);
    Task<Rating?> GetRatingById(Guid id);
    Task<IEnumerable<Rating>> GetRatings();
    Task SaveChanges();
    Task<Rating?> GetRatingByUserIdAndProductId(Guid userId, Guid ProductId);
}
