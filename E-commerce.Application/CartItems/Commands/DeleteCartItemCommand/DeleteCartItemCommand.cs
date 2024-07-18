using MediatR;
using System.Text.Json.Serialization;

namespace E_commerce.Application.CartItems.Commands.DeleteCartItemCommand;
public class DeleteCartItemCommand(Guid cartItemId) : IRequest
{
    [JsonIgnore]
    public Guid CartItemId { get; } = cartItemId;
}
