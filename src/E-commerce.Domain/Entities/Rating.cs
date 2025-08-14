using E_commerce.Domain.Constants;
using E_commerce.Domain.Interfaces;

namespace E_commerce.Domain.Entities;

public class Rating : IUserOwned
{
    public Guid Id { get; set; }
    public User User { get; set; } = default!;
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public DateTime AddedDate { get; set; }
    public Ratings Rate { get; set; }
    public string? Comment { get; set; }
}
