using E_commerce.Application.CartItems.Commands.CreateCartItem;
using E_commerce.Application.CartItems.Commands.DeleteCartItemCommand;
using E_commerce.Application.CartItems.Commands.UpdateCartItemCommand;
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
    public async Task<ActionResult> CreateCartItem(CreateCartItemCommand command)
    {
        await _sender.Send(command);
        return NoContent();
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetUserCartItems(Guid userId)
    {
        var cartItems = await _sender.Send(new GetUserCartItemsQuery(userId));
        return Ok(cartItems);
    }

    [HttpPatch("{cartItemId}")]
    public async Task<ActionResult> UpdateCartItem([FromRoute] Guid cartItemId, UpdateCartItemCommand command)
    {
        command.CartItemId = cartItemId;
        await _sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{cartItemId}")]
    public async Task<ActionResult> DeleteCartItem([FromRoute] Guid cartItemId)
    {
        await _sender.Send(new DeleteCartItemCommand(cartItemId));
        return NoContent();
    }
}
