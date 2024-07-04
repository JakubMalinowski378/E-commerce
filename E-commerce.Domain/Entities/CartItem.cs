namespace E_commerce.Domain.Entities;

public class CartItem
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public virtual Cart Cart { get; set; } = default!;
    public virtual Product Product { get; set; } = default!;
}