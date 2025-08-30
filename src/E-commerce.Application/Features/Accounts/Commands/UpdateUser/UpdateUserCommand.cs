using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Guid>
{
    public string Firstname { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public char Gender { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
