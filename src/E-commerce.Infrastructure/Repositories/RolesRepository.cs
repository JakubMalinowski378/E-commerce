using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;
public class RolesRepository(ECommerceDbContext dbContext) : IRolesRepository
{
    private readonly ECommerceDbContext _dbContext = dbContext;

    public async Task<Role?> GetRole(string RoleName)
        => await _dbContext.Roles
        .FirstOrDefaultAsync(x => x.Name == RoleName);
}
