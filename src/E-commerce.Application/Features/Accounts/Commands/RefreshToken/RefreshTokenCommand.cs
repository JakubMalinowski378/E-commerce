using E_commerce.Application.Features.Accounts.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthResponse>
{
    public string RefreshToken { get; set; } = string.Empty;
}
