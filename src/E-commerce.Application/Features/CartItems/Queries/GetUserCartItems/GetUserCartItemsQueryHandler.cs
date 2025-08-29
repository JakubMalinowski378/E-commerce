using AutoMapper;
using E_commerce.Application.Features.CartItems.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.CartItems.Queries.GetUserCartItems;

public class GetUserCartItemsQueryHandler(
    IRepository<CartItem> cartItemRepository,
    IMapper mapper)
    : IRequestHandler<GetUserCartItemsQuery, IEnumerable<CartItemDto>>
{
    public async Task<IEnumerable<CartItemDto>> Handle(GetUserCartItemsQuery request, CancellationToken cancellationToken)
    {
        var cartItems = await cartItemRepository.FindAsync(ci => ci.UserId == request.UserId);
        var cartItemDtos = mapper.Map<IEnumerable<CartItemDto>>(cartItems);
        return cartItemDtos;
    }
}
