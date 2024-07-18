using E_commerce.Application.Addresses.Commands.DeleteAddress;
using E_commerce.Application.Addresses.Commands.Dtos;
using E_commerce.Application.Addresses.Commands.UpdateAddress;
using E_commerce.Application.Addresses.Queries.GetAddressById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

public class AddressesController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [HttpGet("{addressId}")]
    public async Task<ActionResult<AddressDto>> GetAddressById([FromRoute] Guid addressId)
    {
        var address = await _sender.Send(new GetAddressByIdQuery(addressId));
        return Ok(address);
    }

    [HttpDelete("{addressId}")]
    public async Task<ActionResult> DeleteAddress([FromRoute] Guid addressId)
    {
        await _sender.Send(new DeleteAddressCommand(addressId));
        return NoContent();
    }

    [HttpPatch("{addressId}")]
    public async Task<ActionResult> UpdateAddress([FromRoute] Guid addressId, UpdateAddressCommand command)
    {
        command.AddressId = addressId;
        await _sender.Send(command);
        return NoContent();
    }
}
