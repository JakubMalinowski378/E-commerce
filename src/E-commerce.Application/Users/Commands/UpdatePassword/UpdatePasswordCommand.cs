using MediatR;

namespace E_commerce.Application.Users.Commands.UpdatePassword;

public class UpdatePasswordCommand : IRequest
{
    public string Email { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
