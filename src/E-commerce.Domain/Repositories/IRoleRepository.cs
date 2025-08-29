using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(
        string name,
        Func<IQueryable<Role>, IQueryable<Role>>? include = null,
        bool asNoTracking = false);
}
