using E_commerce.Application.Features.Accounts.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.RegisterUser;
public class RegisterUserCommand : IRequest<AuthResponse>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public char Gender { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
