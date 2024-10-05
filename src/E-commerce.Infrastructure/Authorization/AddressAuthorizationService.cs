using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Interfaces;

namespace E_commerce.Infrastructure.Authorization;
public class AddressAuthorizationService(IUserContext userContext) : IAddressAuthorizationService
{
    private readonly IUserContext _userContext = userContext;

    public bool Authorize(Address address, ResourceOperation resourceOperation)
    {
        var user = _userContext.GetCurrentUser();
        if (resourceOperation == ResourceOperation.Create
            && (user!.Id == address.UserId || user!.IsInRole(UserRoles.Admin)))
        {
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete
            && (user!.Id == address.UserId || user!.IsInRole(UserRoles.Admin)))
        {
            return true;
        }

        if (resourceOperation == ResourceOperation.Update
            && (user!.Id == address.UserId || user!.IsInRole(UserRoles.Admin)))
        {
            return true;
        }

        if (resourceOperation == ResourceOperation.Read
            && (user!.Id == address.UserId || user!.IsInRole(UserRoles.Admin)))
        {
            return true;
        }

        return false;
    }
}
