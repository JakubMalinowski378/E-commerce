using E_commerce.Application.Features.Users;
using E_commerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace E_commerce.Application.Services;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User
            ?? throw new InvalidOperationException("User context is not present");

        if (user.Identity == null || !user.Identity.IsAuthenticated)
            return null;

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;

        return new CurrentUser(Guid.Parse(userId), email, role);
    }
}
