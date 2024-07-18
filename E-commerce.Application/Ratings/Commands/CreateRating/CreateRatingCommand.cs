using MediatR;
using System.Text.Json.Serialization;

namespace E_commerce.Application.Ratings.Commands.CreateRating;
public class CreateRatingCommand : IRequest
{
    [JsonIgnore]
    public Guid ProductId { get; set; }
    public int Rate { get; set; }
    public string? Comment { get; set; } = string.Empty;
}
