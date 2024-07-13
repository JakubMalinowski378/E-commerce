using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Interfaces;

namespace E_commerce.Infrastructure.Authorization;
public class CartItemAuthorizationService(IUserContext userContext) : ICartItemAuthorizationService
{
    private readonly IUserContext _userContext = userContext;

    public bool Authorize(CartItem cartItem)
    {
        var user = _userContext.GetCurrentUser();

        if (cartItem.UserId == user!.Id || user.IsInRole("Admin"))
        {
            return true;
        }
        return false;
    }
}
