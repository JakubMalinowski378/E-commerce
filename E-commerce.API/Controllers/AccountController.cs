using E_commerce.Application.Users.Commands.DeleteUser;
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
    [HttpDelete("{userId}")]
    public async Task<ActionResult> Delete([FromRoute] Guid userId)
    {
        await _sender.Send(new DeleteUserCommand(userId));
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> UpdatePassword(UpdatePasswordCommand command)
    {
        await _sender.Send(command);
        return NoContent();
    }
}
