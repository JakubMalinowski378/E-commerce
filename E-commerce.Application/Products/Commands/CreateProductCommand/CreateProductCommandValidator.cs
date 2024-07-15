using FluentValidation;

namespace E_commerce.Application.Products.Commands.CreateProductCommand;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Supplier)
            .NotEmpty();
    }
}
