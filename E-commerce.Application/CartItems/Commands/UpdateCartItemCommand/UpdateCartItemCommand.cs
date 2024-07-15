using MediatR;
using System.Text.Json.Serialization;

namespace E_commerce.Application.CartItems.Commands.UpdateCartItemCommand;
public class UpdateCartItemCommand : IRequest
{
    [JsonIgnore]
    public Guid CartItemId { get; set; }
    public int Quantity { get; set; }
}
