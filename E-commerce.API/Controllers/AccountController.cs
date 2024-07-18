using E_commerce.Application.Email.Commands.ConfirmEmail;
using E_commerce.Application.Email.Commands.ForgotPassword;
using E_commerce.Application.Email.Commands.ResetPassword;
using E_commerce.Application.Users.Commands.RegisterUser;
using E_commerce.Application.Users.Commands.UpdatePassword;
using E_commerce.Application.Users.Dtos;
using E_commerce.Application.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

public class AccountController(ISender sender) : BaseController
{
    public readonly ISender _sender = sender;

    [HttpPost("register")]

    public async Task<ActionResult<Guid>> Register(RegisterUserCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginUserQuery loginUserQuery)
    {
        var userDto = await _sender.Send(loginUserQuery);
        return Ok(userDto);
    }

    [Authorize]
    [HttpPatch("update-password")]
    public async Task<ActionResult> UpdatePassword(UpdatePasswordCommand command)
    {
        await _sender.Send(command);
        return NoContent();
    }

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
