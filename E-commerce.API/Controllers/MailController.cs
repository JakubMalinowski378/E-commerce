using E_commerce.Application.Email.Commands.ConfirmEmail;
using E_commerce.Application.Email.Commands.ForgotPassword;
using E_commerce.Application.Email.Commands.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

public class MailController(ISender sender) : BaseController
{
    public readonly ISender _sender = sender;
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
    {
        await _sender.Send(new ConfirmEmailCommand(token));
        return Ok("Email confirmed successfully.");
    }
    [HttpGet("forgot-password/{email}")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        await _sender.Send(new ForgotPasswordCommand(email));
        return Ok("Password reset successfully");
    }
    [HttpPost("reset-pasword")]
    public async Task<IActionResult> ResetPasswor([FromQuery] string token, [FromBody] string password)
    {
        await _sender.Send(new ResetPasswordCommand(token, password));
        return Ok("Password reset successfully");
    }
}
