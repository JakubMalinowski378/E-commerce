using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Interfaces;

namespace E_commerce.Infrastructure.Authorization;
public class RatingAuthorizationService(IUserContext userContext) : IRatingAuthorizationService
{
    private readonly IUserContext _userContext = userContext;

    public bool Authorize(Rating rating)
    {
        var user = _userContext.GetCurrentUser();
        if (rating.UserId == user!.Id || user.IsInRole(UserRoles.Admin))
        {
            return true;
        }
        return false;
    }
}
