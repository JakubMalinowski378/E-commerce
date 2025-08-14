using E_commerce.Application.Features.Ratings.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Ratings.Queries.GetProductRatings;
public class GetProductRatingsQuery(Guid productId) : IRequest<IEnumerable<RatingDto>>
{
    public Guid ProductId { get; set; } = productId;
}
