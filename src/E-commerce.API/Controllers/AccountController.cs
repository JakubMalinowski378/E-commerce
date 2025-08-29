using E_commerce.Application.Features.Accounts.Commands.ConfirmEmail;
using E_commerce.Application.Features.Accounts.Commands.ForgotPassword;
using E_commerce.Application.Features.Accounts.Commands.Login;
using E_commerce.Application.Features.Accounts.Commands.Logout;
using E_commerce.Application.Features.Accounts.Commands.RefreshToken;
using E_commerce.Application.Features.Accounts.Commands.RegisterUser;
using E_commerce.Application.Features.Accounts.Commands.ResetPassword;
using E_commerce.Application.Features.Accounts.Commands.UpdatePassword;
using E_commerce.Application.Features.Accounts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AuthResponse>> Register(RegisterUserCommand command)
    {
        return Ok(await sender.Send(command));
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Login(LoginCommand loginUserQuery)
    {
        var userDto = await sender.Send(loginUserQuery);
        return Ok(userDto);
    }

    [Authorize]
    [HttpPut("update-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdatePassword(UpdatePasswordCommand command)
    {
        await sender.Send(command);
        return NoContent();
    }

    [HttpGet("confirm-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
    {
        await sender.Send(new ConfirmEmailCommand(token));
        return Ok("Email confirmed successfully.");
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
    {
        await sender.Send(command);
        return Ok("Password reset successfully");
    }

    [HttpPost("reset-pasword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] ResetPasswordCommand command)
    {
        command.Token = token;
        await sender.Send(command);
        return Ok("Password reset successfully");
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokenCommand query)
    {
        return Ok(await sender.Send(query));
    }

    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        await sender.Send(new LogoutCommand());
        return Ok();
    }
}
