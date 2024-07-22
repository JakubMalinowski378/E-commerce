namespace E_commerce.Application.Products.Dtos;
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public List<string> ProductCategories { get; set; } = default!;
}
