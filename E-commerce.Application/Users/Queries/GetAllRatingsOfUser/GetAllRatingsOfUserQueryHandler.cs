using AutoMapper;
using E_commerce.Application.Ratings.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Users.Queries.GetAllRatingsOfUser;
internal class GetAllRatingsOfUserQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetAllRatingsOfUserQuery, IEnumerable<RatingDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserRepository _userRepository = userRepository;
    async Task<IEnumerable<RatingDto>> IRequestHandler<GetAllRatingsOfUserQuery, IEnumerable<RatingDto>>.Handle(GetAllRatingsOfUserQuery request, CancellationToken cancellationToken)
    {
        var ratings = await _userRepository.GetAllRatingsOfUser(request.Id);
        if (ratings == null)
            throw new NotFoundException(nameof(Rating), request.Id.ToString());
        var ratingsDtos = new List<RatingDto>();
        foreach (var rating in ratings)
        {
            var ratingDto = _mapper.Map<RatingDto>(rating);
            ratingsDtos.Add(ratingDto);
        }
        return ratingsDtos;

    }
}
