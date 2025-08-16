using E_commerce.Application.Interfaces;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace E_commerce.Infrastructure.Services;

internal class AuthorizationService(
    IUserContext userContext,
    IRoleRepository roleRepository,
    IMemoryCache memoryCache) : IAuthorizationService
{
    public async Task<bool> HasPermission(IUserOwned resource, string action)
    {
        var user = userContext.GetCurrentUser()
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var rolePermissions = await memoryCache.GetOrCreateAsync("UserRoles", async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await roleRepository.GetAllRolesWithPermissions();
        });

        var userRole = await roleRepository.GetRole(user.Role)
            ?? throw new UnauthorizedAccessException($"Role '{user.Role}' not found.");

        return action switch
        {
            "View" => true,// All users can view resources.
            "Edit" or "Delete" => resource.UserId == user.Id,// Only the owner can edit or delete.
            _ => throw new NotSupportedException($"Action '{action}' is not supported."),
        };
    }
}
