using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Ratings.Commands.CreateRating;

public class CreateRatingCommandHandler(
    IMapper mapper,
    IRatingRepository ratingRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateRatingCommand>
{
    public async Task Handle(CreateRatingCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        if (await ratingRepository.HasUserRatedProductAsync(user!.Id, request.ProductId))
            throw new InvalidOperationException("You have already rated this product.");

        var rating = mapper.Map<Rating>(request);
        rating.UserId = user!.Id;
        rating.AddedDate = DateTime.UtcNow;
        await ratingRepository.AddAsync(rating);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
