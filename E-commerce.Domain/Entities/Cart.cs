namespace E_commerce.Domain.Entities;
public class Cart
{
    public Guid Id { get; set; }
    public virtual User User { get; set; } = default!;
    public Guid UserId { get; set; }
    public virtual List<CartItem> CartItems { get; set; } = default!;
}

