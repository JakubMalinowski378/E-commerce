using E_commerce.Application.Users.Dtos;
using E_commerce.Application.Users.Queries.GetUserById;
using E_commerce.Application.Users.Queries.GetUsers;
using E_commerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace E_commerce.API.Controllers;

public class UserController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _sender.Send(new GetUsersQuery());
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid id)
    {
        var user = await _sender.Send(new GetUserByIdQuery(id));
        return Ok(user);
    }
}
