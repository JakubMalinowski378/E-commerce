using System.Text.Json.Serialization;

namespace E_commerce.Application.Features.Products.Dtos;
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public List<string> ProductCategories { get; set; } = default!;
    public List<string> ImageUrls { get; set; } = default!;
    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();
}
