using E_commerce.Application.Email.ConfirmEmail.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

public class MailController(ISender sender) : BaseController
{
    public readonly ISender _sender = sender;
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
    {
        var command = new ConfirmEmailCommand(token, email);
        await _sender.Send(command);
        return Ok("Email confirmed successfully.");
    }
}
