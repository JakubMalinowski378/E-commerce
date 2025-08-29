using AutoMapper;
using E_commerce.Application.Features.Ratings.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Users.Queries.GetUserRatingsQuery;

public class GetUserRatingsQueryHandler(
    IRatingRepository ratingRepository,
    IMapper mapper)
    : IRequestHandler<GetUserRatingsQuery, IEnumerable<RatingDto>>
{
    public async Task<IEnumerable<RatingDto>> Handle(GetUserRatingsQuery request, CancellationToken cancellationToken)
    {
        var ratings = await ratingRepository.FindAsync(r => r.UserId == request.UserId)
            ?? throw new NotFoundException(nameof(Rating), request.UserId.ToString());

        var ratingsDtos = mapper.Map<IEnumerable<RatingDto>>(ratings);

        return ratingsDtos;
    }
}
