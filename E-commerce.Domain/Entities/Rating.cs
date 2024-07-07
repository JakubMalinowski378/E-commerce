using E_commerce.Domain.Constants;

namespace E_commerce.Domain.Entities;
public class Rating
{
    public Guid Id { get; set; }
    public virtual User User { get; set; } = default!;
    public Guid UserId { get; set; }
    public virtual Product Product { get; set; } = default!;
    public Guid ProductId { get; set; }
    public DateTime AddedDate { get; set; }
    public Ratings Rate { get; set; }
    public string Comment { get; set; } = string.Empty!;
}
