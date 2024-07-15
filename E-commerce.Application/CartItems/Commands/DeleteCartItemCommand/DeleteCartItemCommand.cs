using MediatR;

namespace E_commerce.Application.CartItems.Commands.DeleteCartItemCommand;
public class DeleteCartItemCommand(Guid cartItemId) : IRequest
{
    public Guid CartItemId { get; } = cartItemId;
}
