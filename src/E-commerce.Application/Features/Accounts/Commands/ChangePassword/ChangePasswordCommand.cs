using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
