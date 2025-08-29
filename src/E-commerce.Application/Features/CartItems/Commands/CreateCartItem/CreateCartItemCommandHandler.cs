using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.CartItems.Commands.CreateCartItem;

public class CreateCartItemCommandHandler(
    IRepository<CartItem> cartItemRepository,
    IRepository<Product> productRepository,
    IUserContext userContext,
    IMapper mapper)
    : IRequestHandler<CreateCartItemCommand>
{
    public async Task Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());

        var user = userContext.GetCurrentUser()
            ?? throw new ForbidException();

        var cartItem = mapper.Map<CartItem>(request);
        cartItem.UserId = user!.Id;
        await cartItemRepository.AddAsync(cartItem);
    }
}
