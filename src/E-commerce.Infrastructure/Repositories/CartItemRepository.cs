using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;
public class CartItemRepository(EcommerceDbContext dbContext) : ICartItemRepository
{
    private readonly EcommerceDbContext _dbContext = dbContext;

    public async Task CreateCartItem(CartItem cartItem)
    {
        _dbContext.CartItems.Add(cartItem);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCartItem(CartItem cartItem)
    {
        _dbContext.CartItems.Remove(cartItem);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CartItem?> GetCartItemByIdAsync(Guid id)
        => await _dbContext.CartItems.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<CartItem>> GetUserCartItemsAsync(Guid userId)
        => await _dbContext.CartItems.Where(x => x.UserId == userId).ToListAsync();

    public Task SaveChanges()
        => _dbContext.SaveChangesAsync();
}
