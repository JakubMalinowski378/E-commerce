using E_commerce.Application.CartItems.Commands.CreateCartItem;
using E_commerce.Application.CartItems.Dtos;
using E_commerce.Application.CartItems.Queries.GetUserCartItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[Authorize]
public class CartItemController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult> CreateCartItem(CreateCartItemCommand createCartItemCommand)
    {
        await _mediator.Send(createCartItemCommand);
        return NoContent();
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetUserCartItems(Guid userId)
    {
        var cartItems = await _mediator.Send(new GetUserCartItemsQuery(userId));
        return Ok(cartItems);
    }
}
