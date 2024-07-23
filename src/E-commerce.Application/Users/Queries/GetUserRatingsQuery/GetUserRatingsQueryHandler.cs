using AutoMapper;
using E_commerce.Application.Ratings.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Users.Queries.GetUserRatingsQuery;
public class GetUserRatingsQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUserRatingsQuery, IEnumerable<RatingDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserRepository _userRepository = userRepository;
    async Task<IEnumerable<RatingDto>> IRequestHandler<GetUserRatingsQuery, IEnumerable<RatingDto>>.Handle(GetUserRatingsQuery request, CancellationToken cancellationToken)
    {
        var ratings = await _userRepository.GetAllRatingsOfUser(request.Id)
            ?? throw new NotFoundException(nameof(Rating), request.Id.ToString());

        var ratingsDtos = _mapper.Map<IEnumerable<RatingDto>>(ratings);

        return ratingsDtos;
    }
}
