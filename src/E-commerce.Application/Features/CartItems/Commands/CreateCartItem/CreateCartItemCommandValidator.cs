using FluentValidation;

namespace E_commerce.Application.Features.CartItems.Commands.CreateCartItem;
public class CreateCartItemCommandValidator : AbstractValidator<CreateCartItemCommand>
{
    public CreateCartItemCommandValidator()
    {
        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}
