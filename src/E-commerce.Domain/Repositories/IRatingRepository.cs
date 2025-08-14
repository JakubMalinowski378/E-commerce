using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface IRatingRepository
{
    Task CreateRating(Rating rating);
    Task DeleteRating(Rating rating);
    Task<Rating?> GetRatingById(Guid ratingId);
    Task<IEnumerable<Rating>> GetRatings();
    Task<IEnumerable<Rating>> GetProductRatings(Guid productId);
    Task<Rating?> GetRatingByUserIdAndProductId(Guid userId, Guid ProductId);
}
