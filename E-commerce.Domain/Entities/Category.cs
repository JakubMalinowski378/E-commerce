namespace E_commerce.Domain.Entities;
public class Category
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = string.Empty!;
    public virtual List<Product> Products { get; set; } = default!;
}
