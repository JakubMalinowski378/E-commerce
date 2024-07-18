using MediatR;

namespace E_commerce.Application.Email.Commands.ConfirmEmail;
public class ConfirmEmailCommand(string token) : IRequest
{
    public string Token { get; set; } = token;
}
