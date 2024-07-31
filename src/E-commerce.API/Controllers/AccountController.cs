using E_commerce.Application.Email.Commands.ConfirmEmail;
using E_commerce.Application.Email.Commands.ForgotPassword;
using E_commerce.Application.Email.Commands.ResetPassword;
using E_commerce.Application.Users.Commands.RegisterByEmail;
using E_commerce.Application.Users.Commands.UpdatePassword;
using E_commerce.Application.Users.Commands.SetLogin;

using E_commerce.Application.Users.Dtos;
using E_commerce.Application.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Application.Users.Commands.ChangeEmail;
using E_commerce.Application.Users.Commands.SetNameAndLastName;
using E_commerce.Application.Users.Commands.SetGender;
using E_commerce.Application.Users.Commands.SetPhoneNumber;

namespace E_commerce.API.Controllers;

public class AccountController(ISender sender) : BaseController
{
    public readonly ISender _sender = sender;

    [HttpPost("registerByEmail")]

    public async Task<ActionResult<Guid>> RegisterByEmail(RegisterByEmailCommand command)
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
    [Authorize]
    [HttpPatch("set-login")]
    public async Task<ActionResult> SetLogin(SetLoginCommand command)
    {
        await _sender.Send(command);
        return Ok("Login set successfully");
    }
    [HttpPatch("set-nameAndLastName")]
    public async Task<ActionResult> SetNameAndLastName(SetNameAndLastNameCommand command)
    {
        await _sender.Send(command);
        return Ok("Login set successfully");
    }
    [Authorize]
    [HttpPatch("set-phone-number")]
    public async Task<ActionResult>SetPhoneNumber(SetPhoneNumberCommand command)
    {
        await _sender.Send(command);
        return Ok("phone number set successfully");
    }

    [Authorize]
    [HttpPatch("set-gender")]
    public async Task<ActionResult>SetGender(SetGenderCommand command)
    {
        await _sender.Send(command);
        return Ok("Gender set successfully");
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
    {
        await _sender.Send(new ConfirmEmailCommand(token));
        return Ok("Email confirmed successfully.");
    }
    [Authorize]
    [HttpGet("change-email")]
    public async Task<IActionResult> ChangeEmail(ChangeEmailCommand changeEmailCommand)
    {
        await _sender.Send(changeEmailCommand);
        return Ok("Email changed successfully.");
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
