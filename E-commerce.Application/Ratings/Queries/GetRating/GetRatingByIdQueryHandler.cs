using AutoMapper;
using E_commerce.Application.Ratings.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Ratings.Queries.GetRating;
public class GetRatingByIdQueryHandler(IRatingRepository ratingRepository, IMapper mapper) : IRequestHandler<GetRatingByIdQuery, RatingDto>
{
    private readonly IRatingRepository _ratingRepository = ratingRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<RatingDto> Handle(GetRatingByIdQuery request, CancellationToken cancellationToken)
    {
        var rating = await _ratingRepository.GetRatingById(request.RatingId);
        if (rating == null)
            throw new NotFoundException(nameof(Rating), request.RatingId.ToString());
        var ratingDto = _mapper.Map<RatingDto>(rating);
        return ratingDto;
    }
}
