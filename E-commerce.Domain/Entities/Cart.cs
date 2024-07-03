namespace E_commerce.Domain.Entities;
public class Cart
{
    public List<(int, Product)> _Cart { get; set; } = default!;
    public User _User { get; set; } = default!;
}

