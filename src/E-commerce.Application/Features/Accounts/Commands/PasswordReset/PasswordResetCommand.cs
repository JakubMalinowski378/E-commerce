using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.PasswordReset;

public class PasswordResetCommand : IRequest
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
