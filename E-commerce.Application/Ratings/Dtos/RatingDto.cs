namespace E_commerce.Application.Ratings.Dtos;
public class RatingDto
{
    public Guid ProductId { get; set; }
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    public int Rate { get; set; }
    public string Comment { get; set; } = string.Empty;
}
