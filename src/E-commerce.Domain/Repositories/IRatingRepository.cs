using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;

public interface IRatingRepository : IRepository<Rating>
{
    Task<bool> HasUserRatedProductAsync(Guid userId, Guid productId);
}
