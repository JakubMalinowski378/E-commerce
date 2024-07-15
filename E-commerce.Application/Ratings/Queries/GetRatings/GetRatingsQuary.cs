using E_commerce.Application.Ratings.Dtos;
using MediatR;

namespace E_commerce.Application.Ratings.Queries.GetRatings;
public class GetRatingsQuary : IRequest<IEnumerable<RatingDto>>
{
}
