using MediatR;

namespace E_commerce.Application.Ratings.Commands.DeleteRating;
public class DeleteRatingCommand(Guid id) : IRequest
{
    public readonly Guid Id = id;
}
