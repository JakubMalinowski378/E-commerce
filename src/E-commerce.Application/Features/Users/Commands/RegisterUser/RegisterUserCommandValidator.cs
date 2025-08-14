using E_commerce.Domain.Repositories;
using FluentValidation;
using System.Text.RegularExpressions;

namespace E_commerce.Application.Features.Users.Commands.RegisterUser;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly char[] _avaiableGenders = ['M', 'F'];
    public RegisterUserCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(x => _avaiableGenders.Contains(x)).WithMessage("Gender must be M or F");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(new Regex(@"(\+48\s?)?(\d{3}[-\s]?\d{3}[-\s]?\d{3})"))
            .WithMessage("PhoneNumber not valid")
            .MustAsync(async (phoneNumber, cancellationToken) => !await userRepository.IsPhoneNumberInUse(phoneNumber))
            .WithMessage("Phone number is already in use");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
    }
}
