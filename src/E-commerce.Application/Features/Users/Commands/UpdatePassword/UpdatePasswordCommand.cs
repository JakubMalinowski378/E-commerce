using MediatR;

namespace E_commerce.Application.Features.Users.Commands.UpdatePassword;

public class UpdatePasswordCommand : IRequest
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
