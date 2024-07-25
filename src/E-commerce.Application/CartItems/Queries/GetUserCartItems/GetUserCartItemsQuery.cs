using E_commerce.Application.CartItems.Dtos;
using MediatR;

namespace E_commerce.Application.CartItems.Queries.GetUserCartItems;
public class GetUserCartItemsQuery(Guid userId) : IRequest<IEnumerable<CartItemDto>>
{
    public Guid UserId { get; set; } = userId;
}
