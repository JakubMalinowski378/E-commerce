using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.CartItems.Commands.DeleteCartItemCommand;
public class DeleteCartItemCommandHandler(ICartItemRepository cartItemRepository,
    ICartItemAuthorizationService cartItemAuthorizationService)
    : IRequestHandler<DeleteCartItemCommand>
{
    private readonly ICartItemRepository _cartItemRepository = cartItemRepository;
    private readonly ICartItemAuthorizationService _cartItemAuthorizationService = cartItemAuthorizationService;

    public async Task Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await _cartItemRepository.GetCartItemByIdAsync(request.CartItemId)
            ?? throw new NotFoundException(nameof(CartItem), request.CartItemId.ToString());

        if (!_cartItemAuthorizationService.Authorize(cartItem))
        {
            throw new ForbidException();
        }

        await _cartItemRepository.DeleteCartItem(cartItem);
    }
}
