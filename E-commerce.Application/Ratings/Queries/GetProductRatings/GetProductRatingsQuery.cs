using E_commerce.Application.Ratings.Dtos;
using MediatR;

namespace E_commerce.Application.Ratings.Queries.GetProductRatings;
public class GetProductRatingsQuery(Guid productId) : IRequest<IEnumerable<RatingDto>>
{
    public Guid ProductId { get; set; } = productId;
}
