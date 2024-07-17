using MediatR;

namespace E_commerce.Application.Email.ConfirmEmail.Commands;
public class ConfirmEmailCommand(string token, string email) : IRequest
{
    public string Token { get; set; } = token;
    public string Email { get; set; } = email;
}
