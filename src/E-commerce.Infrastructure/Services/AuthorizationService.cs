using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace E_commerce.Infrastructure.Services;

internal class AuthorizationService(
    IUserContext userContext,
    IRoleRepository roleRepository,
    IMemoryCache memoryCache,
    ILogger<AuthorizationService> logger) : IAuthorizationService
{
    public async Task<bool> HasPermission(IUserOwned resource, string action)
    {
        var user = userContext.GetCurrentUser()
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var rolePermissions = await memoryCache.GetOrCreateAsync(user.Role, async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60 * 24);
            return await roleRepository.GetByNameAsync(user.Role, r => r.Include(x => x.Permisisons), true);
        });

        switch (action)
        {
            case ResourceOperation.Create:
                return rolePermissions!.Permisisons
                    .Any(r => r.Resource == nameof(resource) && r.Action == ResourceOperation.Create);
            case ResourceOperation.Read:
                return rolePermissions!.Permisisons
                    .Any(r => r.Resource == nameof(resource) && (r.Action == ResourceOperation.Read || r.Action == ResourceOperation.ReadHidden))
                    || (rolePermissions.Permisisons
                        .Any(r => r.Resource == nameof(resource) && r.Action == ResourceOperation.ReadOwns)
                        && resource.UserId == user.Id);
            case ResourceOperation.Update:
                return rolePermissions!.Permisisons
                    .Any(r => r.Resource == nameof(resource) && r.Action == ResourceOperation.Update)
                    || (rolePermissions.Permisisons
                        .Any(r => r.Resource == nameof(resource) && r.Action == ResourceOperation.UpdateOwns)
                        && resource.UserId == user.Id);
            case ResourceOperation.Delete:
                return rolePermissions!.Permisisons
                    .Any(r => r.Resource == nameof(resource) && r.Action == ResourceOperation.Delete)
                    || (rolePermissions.Permisisons
                        .Any(r => r.Resource == nameof(resource) && r.Action == ResourceOperation.DeleteOwns)
                        && resource.UserId == user.Id);
            default:
                logger.LogWarning("Unknown action {Action} for resource {Resource}", action, nameof(resource));
                return false;
        }
    }
}
