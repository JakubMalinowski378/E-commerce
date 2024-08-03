namespace E_commerce.Domain.Entities;

public class CartItem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public User User { get; set; } = default!;
    public Product Product { get; set; } = default!;
}