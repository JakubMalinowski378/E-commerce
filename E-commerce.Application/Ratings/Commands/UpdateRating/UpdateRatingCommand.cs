using MediatR;

namespace E_commerce.Application.Ratings.Commands.UpdateRating;
public class UpdateRatingCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Rate { get; set; }
    public string Comment { get; set; } = string.Empty;
}
