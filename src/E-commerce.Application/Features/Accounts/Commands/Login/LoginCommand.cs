using E_commerce.Application.Features.Accounts.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.Login;

public class LoginCommand : IRequest<AuthResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
