using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Ratings.Commands.CreateRating;
public class CreateRatingCommandHandler(IMapper mapper, IRatingRepository rating, IUserContext userContext) : IRequestHandler<CreateRatingCommand>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IRatingRepository _ratingRepository = rating;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(CreateRatingCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();

        var existingRating = await _ratingRepository.GetRatingByUserIdAndProductId(user!.Id, request.ProductId);
        if (existingRating != null)
            throw new InvalidOperationException("You have already rated this product.");

        var rating = _mapper.Map<Rating>(request);
        rating.UserId = user!.Id;
        rating.AddedDate = DateTime.UtcNow;
        await _ratingRepository.CreateRating(rating);
    }
}
