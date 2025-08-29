using E_commerce.Application.Features.Ratings.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Users.Queries.GetUserRatingsQuery;

public class GetUserRatingsQuery(Guid id) : IRequest<IEnumerable<RatingDto>>
{
    public Guid UserId { get; set; } = id;
}
