using MediatR;

namespace E_commerce.Application.Users.Commands.ConfirmEmail;
public class ConfirmEmailCommand(string token) : IRequest
{
    public string Token { get; set; } = token;
}
