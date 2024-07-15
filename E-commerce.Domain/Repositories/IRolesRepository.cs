using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface IRolesRepository
{
    public Task<Role?> GetRole(string roleName);
}
