using FluentValidation;

namespace E_commerce.Application.Features.Addresses.Commands.UpdateAddress;
public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
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
