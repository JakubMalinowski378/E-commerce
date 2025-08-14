using E_commerce.Domain.Interfaces;

namespace E_commerce.Infrastructure.Authorization;

internal class PermissionService : IPermissionService
{
    public bool HasPermission(Guid userId, IUserOwned resource, string action)
    {

    }
}
