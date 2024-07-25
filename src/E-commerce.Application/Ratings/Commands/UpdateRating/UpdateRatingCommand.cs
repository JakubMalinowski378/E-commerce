using MediatR;
using System.Text.Json.Serialization;

namespace E_commerce.Application.Ratings.Commands.UpdateRating;
public class UpdateRatingCommand(Guid ratingId) : IRequest
{
    [JsonIgnore]
    public Guid RatingId { get; set; } = ratingId;
    public int Rate { get; set; }
    public string? Comment { get; set; } = string.Empty;
}
