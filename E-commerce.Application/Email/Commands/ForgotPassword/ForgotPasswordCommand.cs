using MediatR;

namespace E_commerce.Application.Email.Commands.ForgotPassword;
public class ForgotPasswordCommand(string email) : IRequest
{
    public string Email { get; set; } = email;
    public object Scheme { get; internal set; }
}
