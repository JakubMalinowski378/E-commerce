using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;

public class RoleRepository(
    ECommerceDbContext dbContext)
    : Repository<Role>(dbContext), IRoleRepository
{
    public async Task<Role?> GetByNameAsync(
        string name,
        Func<IQueryable<Role>, IQueryable<Role>>? include = null,
        bool asNoTracking = false)
    {
        var query = _dbSet.AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        if (include is not null)
            query = include(query);

        return await query.FirstOrDefaultAsync(r => r.Name == name);
    }
}
