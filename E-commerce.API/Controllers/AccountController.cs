using E_commerce.Application.Users.Commands.DeleteUser;
using E_commerce.Application.Users.Commands.RegisterUser;
using E_commerce.Application.Users.Commands.UpdateUser;
using E_commerce.Application.Users.Dtos;
using E_commerce.Application.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

public class AccountController(ISender sender) : BaseController
{
    public readonly ISender _sender = sender;

    [HttpPost("register")]
    public async Task<ActionResult<Guid>> Register(RegisterUserCommand registerUserCommand)
    {
        return Ok(await _sender.Send(registerUserCommand));
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginUserQuery loginUserQuery)
    {
        var userDto = await _sender.Send(loginUserQuery);
        return Ok(userDto);
    }
    [HttpDelete]
    public async Task<ActionResult<Guid>>Delete(DeleteUserCommand deleteUserCommand)
    {
        var deletedUser = await _sender.Send(deleteUserCommand);
        return Ok(deletedUser);
    }

}
