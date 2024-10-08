﻿using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.CartItems.Commands.CreateCartItem;
public class CreateCartItemCommandHandler(ICartItemRepository cartItemRepository,
    IProductRepository productsRepository,
    IUserContext userContext,
    IMapper mapper)
    : IRequestHandler<CreateCartItemCommand>
{
    private readonly ICartItemRepository _cartItemRepository = cartItemRepository;
    private readonly IProductRepository _productsRepository = productsRepository;
    private readonly IUserContext _userContext = userContext;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetProductByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());

        var user = _userContext.GetCurrentUser()
            ?? throw new ForbidException();

        var cartItem = _mapper.Map<CartItem>(request);
        cartItem.UserId = user!.Id;
        await _cartItemRepository.CreateCartItem(cartItem);
    }
}
