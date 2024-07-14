using AutoMapper;
using E_commerce.Application.Ratings.Dtos;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Ratings.Queries.GetRatings;
public class GetRatingsQueryHandler(IRatingRepository ratingRepository, IMapper mapper) : IRequestHandler<GetRatingsQuary, IEnumerable<RatingDto>>
{
    public readonly IMapper _mapper = mapper;
    public readonly IRatingRepository _ratingRepository = ratingRepository;
    public async Task<IEnumerable<RatingDto>> Handle(GetRatingsQuary request, CancellationToken cancellationToken)
    {
        var ratings = await _ratingRepository.GetRatings();
        List<RatingDto> ratingsDtos = new List<RatingDto>();
        foreach (var rating in ratings)
        {
            var ratingDto = _mapper.Map<RatingDto>(rating);
            ratingsDtos.Add(ratingDto);
        }
        return ratingsDtos;
    }
}
