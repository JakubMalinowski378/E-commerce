namespace E_commerce.Domain.Entities;
public class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = string.Empty!;
    public List<Product> Products { get; set; } = default!;
}
