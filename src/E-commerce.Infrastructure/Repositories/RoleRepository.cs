using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;
public class RoleRepository(ECommerceDbContext dbContext) : IRoleRepository
{
    public async Task<IEnumerable<Role>> GetAllRolesWithPermissions()
        => await dbContext.Roles
            .Include(r => r.Permisisons)
            .ToListAsync();

    public async Task<Role?> GetRole(string RoleName)
        => await dbContext.Roles
            .FirstOrDefaultAsync(x => x.Name == RoleName);
}
