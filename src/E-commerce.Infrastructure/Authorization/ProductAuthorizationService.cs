using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Interfaces;

namespace E_commerce.Infrastructure.Authorization;
public class ProductAuthorizationService(IUserContext userContext)
    : IProductAuthorizationService
{
    private readonly IUserContext _userContext = userContext;

    public bool Authorize(Product product, ResourceOperation resourceOperation)
    {
        var user = _userContext.GetCurrentUser();

        if (resourceOperation == ResourceOperation.Create
            && (user!.IsInRole(UserRoles.Salesman) || user!.IsInRole(UserRoles.Admin)))
        {
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete
            && (product.UserId == user!.Id || user.IsInRole(UserRoles.Admin)))
        {
            return true;
        }

        if (resourceOperation == ResourceOperation.Update
            && (product.UserId == user!.Id || user!.IsInRole(UserRoles.Admin)))
        {
            return true;
        }

        return false;
    }
}
