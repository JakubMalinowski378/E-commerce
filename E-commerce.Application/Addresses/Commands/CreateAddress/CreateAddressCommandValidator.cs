using FluentValidation;

namespace E_commerce.Application.Addresses.Commands.CreateAddress;
public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(x => x.StreetNumber)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.City)
            .NotEmpty();

        RuleFor(x => x.PostalCode)
            .NotEmpty();

        RuleFor(x => x.Street)
            .NotEmpty();
    }
}
