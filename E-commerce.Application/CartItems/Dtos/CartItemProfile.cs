using AutoMapper;
using E_commerce.Application.CartItems.Commands.CreateCartItem;
using E_commerce.Domain.Entities;

namespace E_commerce.Application.CartItems.Dtos;
public class CartItemProfile : Profile
{
    public CartItemProfile()
    {
        CreateMap<CreateCartItemCommand, CartItem>();
        CreateMap<CartItem, CartItemDto>();
    }
}
