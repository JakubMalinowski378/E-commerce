using MediatR;

namespace E_commerce.Application.Email.Commands.ResetPassword;
public class ResetPasswordCommand(string token, string password) : IRequest
{
    public string Token { get; set; } = token;
    public string Password { get; set; } = password;
}
