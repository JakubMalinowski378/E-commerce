using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.CartItems.Commands.UpdateCartItemCommand;

public class UpdateCartItemCommandHandler(
    IUnitOfWork unitOfWork,
    IRepository<CartItem> cartItemRepository,
    IAuthorizationService authorizationService)
    : IRequestHandler<UpdateCartItemCommand>
{
    public async Task Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemRepository.GetByIdAsync(request.CartItemId)
            ?? throw new NotFoundException(nameof(CartItem), request.CartItemId.ToString());

        if (!await authorizationService.HasPermission(cartItem, ResourceOperation.Update))
        {
            throw new ForbidException();
        }

        cartItem.Quantity = request.Quantity;
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
