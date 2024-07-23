using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface ICartItemRepository
{
    Task CreateCartItem(CartItem cartItem);
    Task DeleteCartItem(CartItem cartItem);
    Task<CartItem?> GetCartItemByIdAsync(Guid id);
    Task<IEnumerable<CartItem>> GetUserCartItemsAsync(Guid userId);
    Task SaveChanges();
}
