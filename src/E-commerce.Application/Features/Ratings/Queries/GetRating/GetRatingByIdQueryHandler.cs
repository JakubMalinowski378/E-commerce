using AutoMapper;
using E_commerce.Application.Features.Ratings.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Ratings.Queries.GetRating;

public class GetRatingByIdQueryHandler(
    IRatingRepository ratingRepository,
    IMapper mapper)
    : IRequestHandler<GetRatingByIdQuery, RatingDto>
{
    public async Task<RatingDto> Handle(GetRatingByIdQuery request, CancellationToken cancellationToken)
    {
        var rating = await ratingRepository.GetByIdAsync(request.RatingId)
            ?? throw new NotFoundException(nameof(Rating), request.RatingId.ToString());
        var ratingDto = mapper.Map<RatingDto>(rating);
        return ratingDto;
    }
}
