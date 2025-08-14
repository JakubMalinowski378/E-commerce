using AutoMapper;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Ratings.Commands.UpdateRating;

public class UpdateRatingCommandHandler(
    IRatingRepository ratingRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateRatingCommand>
{
    public readonly IRatingRepository _ratingRepository = ratingRepository;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = await _ratingRepository.GetRatingById(request.RatingId)
            ?? throw new NotFoundException(nameof(Rating), request.RatingId.ToString());

        _mapper.Map(request, rating);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
