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
[ApiController]
[Route("api")]
public class CartItemController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPatch("CartItems/{cartItemId}")]
    public async Task<ActionResult> UpdateCartItem([FromRoute] Guid cartItemId, UpdateCartItemCommand command)
    {
        command.CartItemId = cartItemId;
        await _sender.Send(command);
        return NoContent();
    }

    [HttpDelete("CartItems/{cartItemId}")]
    public async Task<ActionResult> DeleteCartItem([FromRoute] Guid cartItemId)
    {
        await _sender.Send(new DeleteCartItemCommand(cartItemId));
        return NoContent();
    }

    [HttpPost("CartItems")]
    public async Task<ActionResult> CreateCartItem(CreateCartItemCommand command)
    {
        await _sender.Send(command);
        return NoContent();
    }

    [HttpGet("Users/{userId}/CartItems")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetUserCartItems([FromRoute] Guid userId)
    {
        var cartItems = await _sender.Send(new GetUserCartItemsQuery(userId));
        return Ok(cartItems);
    }
}
