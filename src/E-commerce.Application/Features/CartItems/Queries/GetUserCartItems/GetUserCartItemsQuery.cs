using E_commerce.Application.Features.CartItems.Dtos;
using MediatR;

namespace E_commerce.Application.Features.CartItems.Queries.GetUserCartItems;
public class GetUserCartItemsQuery(Guid userId) : IRequest<IEnumerable<CartItemDto>>
{
    public Guid UserId { get; set; } = userId;
}
