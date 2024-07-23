using E_commerce.Application.CartItems.Commands.DeleteCartItemCommand;
using E_commerce.Application.CartItems.Commands.UpdateCartItemCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[Authorize]
public class CartItemController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

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
