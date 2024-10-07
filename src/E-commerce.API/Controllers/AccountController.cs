using E_commerce.Application.Users.Commands.ConfirmEmail;
using E_commerce.Application.Users.Commands.ForgotPassword;
using E_commerce.Application.Users.Commands.RegisterUser;
using E_commerce.Application.Users.Commands.ResetPassword;
using E_commerce.Application.Users.Commands.UpdatePassword;
using E_commerce.Application.Users.Dtos;
using E_commerce.Application.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(ISender sender) : ControllerBase
{
    public readonly ISender _sender = sender;

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<JwtToken>> Register(RegisterUserCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<JwtToken>> Login(LoginUserQuery loginUserQuery)
    {
        var userDto = await _sender.Send(loginUserQuery);
        return Ok(userDto);
    }

    [Authorize]
    [HttpPatch("update-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdatePassword(UpdatePasswordCommand command)
    {
        await _sender.Send(command);
        return NoContent();
    }

    [HttpGet("confirm-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
    {
        await _sender.Send(new ConfirmEmailCommand(token));
        return Ok("Email confirmed successfully.");
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
    {
        await _sender.Send(command);
        return Ok("Password reset successfully");
    }

    [HttpPost("reset-pasword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] ResetPasswordCommand command)
    {
        command.Token = token;
        await _sender.Send(command);
        return Ok("Password reset successfully");
    }
}
