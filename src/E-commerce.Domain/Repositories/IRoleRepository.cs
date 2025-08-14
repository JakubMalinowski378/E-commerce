using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetRole(string roleName);
    Task<IEnumerable<Role>> GetAllRolesWithPermissions();
}
