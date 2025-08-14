using E_commerce.Application.Features.Users.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Users.Queries.LoginUser;
public class LoginUserQuery : IRequest<JwtToken>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
