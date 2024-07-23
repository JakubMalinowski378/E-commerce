using FluentValidation;

namespace E_commerce.Application.Ratings.Commands.CreateRating;
public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
{
    public CreateRatingCommandValidator()
    {
        RuleFor(x => x.Rate)
            .NotEmpty()
            .InclusiveBetween(1, 5);
    }
}
