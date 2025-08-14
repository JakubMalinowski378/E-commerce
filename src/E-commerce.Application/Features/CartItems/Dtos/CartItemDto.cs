namespace E_commerce.Application.Features.CartItems.Dtos;
public class CartItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
