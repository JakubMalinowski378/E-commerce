using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.ConfirmEmail;
public class ConfirmEmailCommand(string token) : IRequest
{
    public string Token { get; set; } = token;
}
