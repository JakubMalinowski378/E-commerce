namespace E_commerce.Application.Features.Ratings.Dtos;
public class RatingDto
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public DateTime AddedDate { get; set; }
    public int Rate { get; set; }
    public string Comment { get; set; } = string.Empty;
}

