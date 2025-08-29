using E_commerce.Application.Features.Users.Commands.DeleteUser;
using E_commerce.Application.Features.Users.Dtos;
using E_commerce.Application.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid userId)
    {
        var user = await sender.Send(new GetUserByIdQuery(userId));
        return Ok(user);
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUser([FromRoute] Guid userId)
    {
        await sender.Send(new DeleteUserCommand(userId));
        return NoContent();
    }
}
