using E_commerce.Domain.Interfaces;

namespace E_commerce.Domain.Entities;

public class Product : IUserOwned
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> ProductImagesUrls { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsHidden { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public List<Category> Categories { get; set; } = default!;
    public IDictionary<string, object> AdditionalProperties { get; set; } = default!;
}
