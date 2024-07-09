namespace E_commerce.Domain.Entities;
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Supplier { get; set; } = string.Empty;
    public virtual List<CartItem> CartItems { get; set; } = default!;
    public virtual List<ProductCategory> ProductCategories { get; set; } = default!;
    public List<Rating> Ratings { get; set; } = default!;
    //public List<Parameter> ParametersList { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public User Owner { get; set; } = default!;
    public Guid OwnerId { get; set; }
}
