using MediatR;

namespace E_commerce.Application.Features.Users.Commands.ConfirmEmail;
public class ConfirmEmailCommand(string token) : IRequest
{
    public string Token { get; set; } = token;
}
