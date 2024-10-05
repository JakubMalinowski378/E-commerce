using E_commerce.Application.Addresses.Commands.CreateAddress;
using E_commerce.Application.Addresses.Commands.DeleteAddress;
using E_commerce.Application.Addresses.Commands.Dtos;
using E_commerce.Application.Addresses.Commands.UpdateAddress;
using E_commerce.Application.Addresses.Queries.GetAddressById;
using E_commerce.Application.Addresses.Queries.GetUserAddresses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class AddressesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("Addresses/{addressId}")]
    public async Task<ActionResult<AddressDto>> GetAddressById([FromRoute] Guid addressId)
    {
        var address = await _sender.Send(new GetAddressByIdQuery(addressId));
        return Ok(address);
    }

    [HttpDelete("Addresses/{addressId}")]
    public async Task<ActionResult> DeleteAddress([FromRoute] Guid addressId)
    {
        await _sender.Send(new DeleteAddressCommand(addressId));
        return NoContent();
    }

    [HttpPatch("Addresses/{addressId}")]
    public async Task<ActionResult> UpdateAddress([FromRoute] Guid addressId, UpdateAddressCommand command)
    {
        command.AddressId = addressId;
        await _sender.Send(command);
        return NoContent();
    }

    [HttpPost("Addresses")]
    public async Task<ActionResult> CreateAddress(CreateAddressCommand command)
    {
        var addressId = await _sender.Send(command);
        return CreatedAtAction(nameof(AddressesController.GetAddressById), new { addressId }, null);
    }

    [HttpGet("Users/{userId}/Addresses")]
    public async Task<ActionResult<AddressDto>> GetUserAddresses([FromRoute] Guid userId)
    {
        var addresses = await _sender.Send(new GetUserAddressesQuery(userId));
        return Ok(addresses);
    }
}
