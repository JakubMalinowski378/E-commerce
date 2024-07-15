using E_commerce.Application.Ratings.Dtos;
using MediatR;

namespace E_commerce.Application.Users.Queries.GetAllRatingsOfUser;
public class GetAllRatingsOfUserQuery(Guid id) : IRequest<IEnumerable<RatingDto>>
{
    public Guid Id { get; set; } = id;
}
