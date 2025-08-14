using E_commerce.Application.Features.Ratings.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Ratings.Queries.GetRating;
public class GetRatingByIdQuery(Guid id) : IRequest<RatingDto>
{
    public Guid RatingId { get; set; } = id;
}
