using MediatR;

namespace E_commerce.Application.Features.CartItems.Commands.CreateCartItem;
public class CreateCartItemCommand : IRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
