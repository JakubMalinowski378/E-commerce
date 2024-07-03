namespace E_commerce.Domain.Entities;
public class Cart
{
    public Guid Id { get; set; }
    //public List<(int, Product)> _Cart { get; set; } = default!;
    public User User { get; set; } = default!;
    public Guid UserId { get; set; }
}

