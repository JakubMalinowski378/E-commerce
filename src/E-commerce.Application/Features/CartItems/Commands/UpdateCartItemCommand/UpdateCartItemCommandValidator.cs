using FluentValidation;

namespace E_commerce.Application.Features.CartItems.Commands.UpdateCartItemCommand;
public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
{
    public UpdateCartItemCommandValidator()
    {
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
    }
}
