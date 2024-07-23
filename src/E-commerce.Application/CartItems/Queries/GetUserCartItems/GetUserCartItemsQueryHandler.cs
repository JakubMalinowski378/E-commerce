using AutoMapper;
using E_commerce.Application.CartItems.Dtos;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.CartItems.Queries.GetUserCartItems;
public class GetUserCartItemsQueryHandler(ICartItemRepository cartItemRepository,
    IMapper mapper)
    : IRequestHandler<GetUserCartItemsQuery, IEnumerable<CartItemDto>>
{
    private readonly ICartItemRepository _cartItemRepository = cartItemRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<CartItemDto>> Handle(GetUserCartItemsQuery request, CancellationToken cancellationToken)
    {
        var cartItems = await _cartItemRepository.GetUserCartItemsAsync(request.UserId);
        var cartItemDtos = _mapper.Map<IEnumerable<CartItemDto>>(cartItems);
        return cartItemDtos;
    }
}
