using E_commerce.Application.Addresses.Commands.CreateAddress;
using E_commerce.Application.CartItems.Commands.CreateCartItem;
using E_commerce.Application.CartItems.Dtos;
using E_commerce.Application.CartItems.Queries.GetUserCartItems;
using E_commerce.Application.Products.Dtos;
using E_commerce.Application.Products.Queries.GetUserProducts;
using E_commerce.Application.Ratings.Dtos;
using E_commerce.Application.Users.Commands.DeleteUser;
using E_commerce.Application.Users.Dtos;
using E_commerce.Application.Users.Queries.GetUserById;
using E_commerce.Application.Users.Queries.GetUserRatingsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;
public class UsersController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid userId)
    {
        var user = await _sender.Send(new GetUserByIdQuery(userId));
        return Ok(user);
    }

    [Authorize]
    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUser([FromRoute] Guid userId)
    {
        await _sender.Send(new DeleteUserCommand(userId));
        return NoContent();
    }

    [Authorize]
    [HttpPost("{userId}/addresses")]
    public async Task<ActionResult> CreateAddress([FromRoute] Guid userId, CreateAddressCommand command)
    {
        command.UserId = userId;
        var addressId = await _sender.Send(command);
        return CreatedAtAction(nameof(AddressesController.GetAddressById), new { addressId }, null);
    }

    [HttpGet("{userId}/ratings")]
    public async Task<ActionResult<IEnumerable<RatingDto>>> GetAllRatingsOfUser(Guid userId)
    {
        var ratings = await _sender.Send(new GetUserRatingsQuery(userId));
        return Ok(ratings);
    }

    [HttpPost("{userId}/cartItems")]
    public async Task<ActionResult> CreateCartItem(CreateCartItemCommand command)
    {
        await _sender.Send(command);
        return NoContent();
    }

    [HttpGet("{userId}/cartItems")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetUserCartItems([FromRoute] Guid userId)
    {
        var cartItems = await _sender.Send(new GetUserCartItemsQuery(userId));
        return Ok(cartItems);
    }

    [HttpGet("{userId}/products")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetUserProducts([FromRoute] Guid userId)
    {
        var products = await _sender.Send(new GetUserProducts(userId));
        return Ok(products);
    }
}
