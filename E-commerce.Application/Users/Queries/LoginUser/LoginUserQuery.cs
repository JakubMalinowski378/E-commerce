using E_commerce.Application.Users.Dtos;
using MediatR;

namespace E_commerce.Application.Users.Queries.LoginUser;
public class LoginUserQuery : IRequest<UserDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}