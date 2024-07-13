using E_commerce.Application.Addresses.Commands.CreateAddress;
using E_commerce.Application.Addresses.Commands.Dtos;
using E_commerce.Application.Addresses.Queries.GetAddressById;
using E_commerce.Application.Users.Dtos;
using E_commerce.Application.Users.Queries.GetUserById;
using E_commerce.Application.Users.Queries.GetUsers;
using E_commerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace E_commerce.API.Controllers;

public class UserController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _sender.Send(new GetUsersQuery());
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid id)
    {
        var user = await _sender.Send(new GetUserByIdQuery(id));
        return Ok(user);
    }

    [HttpGet("address/{addressId}")]
    public async Task<ActionResult<AddressDto>> GetAddressById([FromRoute] Guid addressId)
    {
        var address = await _sender.Send(new GetAddressByIdQuery(addressId));
        return Ok(address);
    }

    [Authorize]
    [HttpPost("address")]
    public async Task<ActionResult> CreateAddress(CreateAddressCommand createAddressCommand)
    {
        var addressId = await _sender.Send(createAddressCommand);
        return CreatedAtAction(nameof(GetAddressById), new { addressId }, null);
    }
}
