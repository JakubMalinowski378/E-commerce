using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.PasswordResetRequest;

public class ResetPasswordRequestCommand : IRequest
{
    public string Email { get; set; } = string.Empty;
}
