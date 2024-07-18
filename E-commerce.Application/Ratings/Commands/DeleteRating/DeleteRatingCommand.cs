using MediatR;

namespace E_commerce.Application.Ratings.Commands.DeleteRating;
public class DeleteRatingCommand(Guid ratingId) : IRequest
{
    public readonly Guid RatingId = ratingId;
}
