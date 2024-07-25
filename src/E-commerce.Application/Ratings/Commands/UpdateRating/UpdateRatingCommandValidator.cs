using FluentValidation;

namespace E_commerce.Application.Ratings.Commands.UpdateRating;
public class UpdateRatingCommandValidator : AbstractValidator<UpdateRatingCommand>
{
    public UpdateRatingCommandValidator()
    {
        RuleFor(x => x.Rate)
            .NotEmpty()
            .InclusiveBetween(1, 5);
    }
}
