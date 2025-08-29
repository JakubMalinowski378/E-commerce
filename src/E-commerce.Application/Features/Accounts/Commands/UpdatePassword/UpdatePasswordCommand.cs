using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.UpdatePassword;

public class UpdatePasswordCommand : IRequest
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
