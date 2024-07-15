using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface IRolesRepository
{
    Task<Role?> GetRole(string roleName);
}
