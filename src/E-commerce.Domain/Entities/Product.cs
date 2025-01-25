using E_commerce.Domain.Helpers;
using MongoDB.Bson.Serialization.Attributes;

namespace E_commerce.Domain.Entities;
public class Product
{
    [BsonId]
    [BsonSerializer(typeof(GuidAsStringSerializer))]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> ProductImages { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsHidden { get; set; }
    [BsonSerializer(typeof(GuidAsStringSerializer))]
    public Guid UserId { get; set; }
    [BsonExtraElements]
    public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();
}
