using E_commerce.Application.Features.Accounts.Commands.ChangePassword;
using E_commerce.Application.Features.Accounts.Commands.ConfirmEmail;
using E_commerce.Application.Features.Accounts.Commands.Login;
using E_commerce.Application.Features.Accounts.Commands.Logout;
using E_commerce.Application.Features.Accounts.Commands.PasswordReset;
using E_commerce.Application.Features.Accounts.Commands.PasswordResetRequest;
using E_commerce.Application.Features.Accounts.Commands.RefreshToken;
using E_commerce.Application.Features.Accounts.Commands.RegisterUser;
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
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        await sender.Send(new LogoutCommand());
        return NoContent();
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokenCommand query)
    {
        return Ok(await sender.Send(query));
    }

    [HttpGet("confirm-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand command)
    {
        await sender.Send(command);
        return Ok("Email confirmed successfully.");
    }

    [Authorize]
    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        await sender.Send(command);
        return NoContent();
    }

    [HttpPost("request-password-reset")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordRequestCommand command)
    {
        await sender.Send(command);
        return Ok("If the account exists, a password reset link has been sent.");
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetPassword([FromBody] PasswordResetCommand command)
    {
        await sender.Send(command);
        return Ok("Password reset successfully");
    }
}
