namespace E_commerce.Domain.Entities;
public class Cart
{
    public Guid _Id { get; set; }
    public List<(int, Product)> _Cart { get; set; } = default!;
    public User _User { get; set; } = default!;
    public Guid _IdUser { get; set; }
}

