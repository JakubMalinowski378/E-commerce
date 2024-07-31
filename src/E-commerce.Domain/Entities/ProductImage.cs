namespace E_commerce.Domain.Entities;
public class ProductImage
{
    public string FileName { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
}
