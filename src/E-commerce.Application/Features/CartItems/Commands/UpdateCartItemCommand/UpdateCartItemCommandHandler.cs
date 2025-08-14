using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.CartItems.Commands.UpdateCartItemCommand;
public class UpdateCartItemCommandHandler(
    ICartItemRepository cartItemRepository,
    ICartItemAuthorizationService cartItemAuthorizationService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCartItemCommand>
{
    private readonly ICartItemRepository _cartItemRepository = cartItemRepository;
    private readonly ICartItemAuthorizationService _cartItemAuthorizationService = cartItemAuthorizationService;

    public async Task Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await _cartItemRepository.GetCartItemByIdAsync(request.CartItemId)
            ?? throw new NotFoundException(nameof(CartItem), request.CartItemId.ToString());

        if (!_cartItemAuthorizationService.Authorize(cartItem))
        {
            throw new ForbidException();
        }

        cartItem.Quantity = request.Quantity;
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
