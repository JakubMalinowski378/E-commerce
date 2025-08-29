using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.CartItems.Commands.DeleteCartItemCommand;

public class DeleteCartItemCommandHandler(
    IRepository<CartItem> cartItemRepository,
    IAuthorizationService authorizationService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCartItemCommand>
{
    public async Task Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemRepository.GetByIdAsync(request.CartItemId)
            ?? throw new NotFoundException(nameof(CartItem), request.CartItemId.ToString());

        if (!await authorizationService.HasPermission(cartItem, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }

        cartItemRepository.Remove(cartItem);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
