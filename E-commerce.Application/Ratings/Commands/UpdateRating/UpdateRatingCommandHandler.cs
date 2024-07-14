using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Ratings.Commands.UpdateRating;
public class UpdateRatingCommandHandler(IRatingRepository ratingRepository) : IRequestHandler<UpdateRatingCommand>
{
    public readonly IRatingRepository _ratingRepository = ratingRepository;
    public async Task Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = await _ratingRepository.GetRatingByUserIdAndProductId(request.UserId, request.ProductId);
        if (rating == null)
        {
            throw new NotFoundException(nameof(Rating), request.UserId.ToString() + " " + request.ProductId.ToString());
        }
        rating.Rate = (Domain.Constants.Ratings)request.Rate;
        rating.Comment = request.Comment;
        await _ratingRepository.SaveChanges();
    }
}
