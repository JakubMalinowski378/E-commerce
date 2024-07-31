using E_commerce.Application.Users.Commands.SetPhoneNumber;
using FluentValidation;
using System.Text.RegularExpressions;

public class SetPhoneNumberCommandValidator : AbstractValidator<SetPhoneNumberCommand>
{
    public SetPhoneNumberCommandValidator()
    {
        RuleFor(contact => contact.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+\d{1,3}\d{4,14}$").WithMessage("Phone number must be in international format, e.g., +1234567890.")
            .Length(10, 15).WithMessage("Phone number must be between 10 and 15 digits long.");
    }
}

