using E_commerce.Application.Features.CartItems.Commands.CreateCartItem;
using E_commerce.Application.Features.CartItems.Commands.DeleteCartItemCommand;
using E_commerce.Application.Features.CartItems.Commands.UpdateCartItemCommand;
using E_commerce.Application.Features.CartItems.Dtos;
using E_commerce.Application.Features.CartItems.Queries.GetUserCartItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class CartItemController(ISender sender) : ControllerBase
{
    [HttpPut("CartItems/{cartItemId}")]
    public async Task<ActionResult> UpdateCartItem([FromRoute] Guid cartItemId, UpdateCartItemCommand command)
    {
        command.CartItemId = cartItemId;
        await sender.Send(command);
        return NoContent();
    }

    [HttpDelete("CartItems/{cartItemId}")]
    public async Task<ActionResult> DeleteCartItem([FromRoute] Guid cartItemId)
    {
        await sender.Send(new DeleteCartItemCommand(cartItemId));
        return NoContent();
    }

    [HttpPost("CartItems")]
    public async Task<ActionResult> CreateCartItem(CreateCartItemCommand command)
    {
        await sender.Send(command);
        return NoContent();
    }

    [HttpGet("Users/{userId}/CartItems")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetUserCartItems([FromRoute] Guid userId)
    {
        var cartItems = await sender.Send(new GetUserCartItemsQuery(userId));
        return Ok(cartItems);
    }
}
