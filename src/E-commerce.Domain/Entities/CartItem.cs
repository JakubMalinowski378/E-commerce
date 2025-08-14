using E_commerce.Domain.Interfaces;

namespace E_commerce.Domain.Entities;

public class CartItem : IUserOwned
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public int Quantity { get; set; }
    public User User { get; set; } = default!;
}