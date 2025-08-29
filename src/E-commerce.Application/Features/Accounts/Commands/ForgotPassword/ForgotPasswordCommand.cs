using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.ForgotPassword;
public class ForgotPasswordCommand(string email) : IRequest
{
    public string Email { get; set; } = email;
}
