using AutoMapper;
using E_commerce.Application.Features.Ratings.Dtos;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Ratings.Queries.GetProductRatings;

public class GetProductRatingsQueryHandler(
    IRatingRepository ratingRepository,
    IMapper mapper)
    : IRequestHandler<GetProductRatingsQuery, IEnumerable<RatingDto>>
{
    public async Task<IEnumerable<RatingDto>> Handle(
        GetProductRatingsQuery request,
        CancellationToken cancellationToken)
    {
        var ratings = await ratingRepository.FindAsync(r => r.ProductId == request.ProductId);
        var ratingDtos = mapper.Map<IEnumerable<RatingDto>>(ratings);
        return ratingDtos;
    }
}
