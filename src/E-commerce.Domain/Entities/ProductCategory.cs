namespace E_commerce.Domain.Entities;
public class ProductCategory
{
    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public Guid ProductId { get; set; }
}
