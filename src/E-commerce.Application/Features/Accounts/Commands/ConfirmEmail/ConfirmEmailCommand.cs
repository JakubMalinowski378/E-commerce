using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
}
