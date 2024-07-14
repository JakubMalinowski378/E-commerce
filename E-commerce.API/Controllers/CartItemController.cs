using E_commerce.Application.CartItems.Commands.CreateCartItem;
using E_commerce.Application.CartItems.Dtos;
using E_commerce.Application.CartItems.Queries.GetUserCartItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[Authorize]
public class CartItemController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<ActionResult> CreateCartItem(CreateCartItemCommand createCartItemCommand)
    {
        await _sender.Send(createCartItemCommand);
        return NoContent();
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetUserCartItems(Guid userId)
    {
        var cartItems = await _sender.Send(new GetUserCartItemsQuery(userId));
        return Ok(cartItems);
    }
}
