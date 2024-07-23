using AutoMapper;
using E_commerce.Application.Ratings.Dtos;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Ratings.Queries.GetProductRatings;
public class GetProductRatingsQueryHandler(IRatingRepository ratingRepository, IMapper mapper)
    : IRequestHandler<GetProductRatingsQuery, IEnumerable<RatingDto>>
{
    private readonly IRatingRepository _ratingRepository = ratingRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<RatingDto>> Handle(GetProductRatingsQuery request, CancellationToken cancellationToken)
    {
        var ratings = await _ratingRepository.GetProductRatings(request.ProductId);
        var ratingDtos = _mapper.Map<IEnumerable<RatingDto>>(ratings);
        return ratingDtos;
    }
}
