using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface IRatingRepository
{
    public Task CreateRating(Rating rating);
    public Task DeleteRating(Rating rating);
    public Task<Rating> GetRatingById(Guid id);
    public Task<IEnumerable<Rating>> GetRatings();
    public Task SaveChanges();

    public Task<Rating> GetRatingByUserIdAndProductId(Guid userId, Guid ProductId);
}
