using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Interfaces;
public interface ICartItemAuthorizationService
{
    bool Authorize(CartItem cartItem);
}
