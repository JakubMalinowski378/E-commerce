using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Ratings.Commands.DeleteRating;

public class DeleteRatingCommandHandler(
    IRatingRepository ratingRepository,
    IUnitOfWork unitOfWork,
    IAuthorizationService authorizationService)
    : IRequestHandler<DeleteRatingCommand>
{
    public async Task Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = await ratingRepository.GetByIdAsync(request.RatingId)
            ?? throw new NotFoundException(nameof(Rating), request.RatingId.ToString());

        if (!await authorizationService.HasPermission(rating, ResourceOperation.Delete))
            throw new ForbidException();

        ratingRepository.Remove(rating);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
