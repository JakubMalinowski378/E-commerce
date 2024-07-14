using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Ratings.Commands.DeleteRating;
public class DeleteRatingCommandHandler(IRatingRepository ratingRepository) : IRequestHandler<DeleteRatingCommand>
{
    public readonly IRatingRepository _ratingRepository = ratingRepository;
    public async Task Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = await _ratingRepository.GetRatingById(request.Id);
        if (rating == null)
            throw new NotFoundException(nameof(Rating), request.Id.ToString());
        await _ratingRepository.DeleteRating(rating);
    }
}
