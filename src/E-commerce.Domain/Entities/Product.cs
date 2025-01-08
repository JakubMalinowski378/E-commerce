using E_commerce.Domain.Helpers;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E_commerce.Domain.Entities;
public class Product
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<CartItem> CartItems { get; set; } = default!;
    public List<Category> Categories { get; set; } = default!;
    public List<Rating> Ratings { get; set; } = default!;
    public List<string> ProductImages { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsHidden { get; set; }
    public User User { get; set; } = default!;
    public Guid UserId { get; set; }
    [BsonExtraElements]
    [JsonExtensionData]
    private IDictionary<string, object> _additionalProperties { get; set; } = new Dictionary<string, object>();

    public IDictionary<string, object> AdditionalProperties
    {
        get => _additionalProperties;
        set
        {
            _additionalProperties = value.ToDictionary(

                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement jsonElement
                    ? SerializationHelper.ConvertJsonElement(jsonElement)
                    : kvp.Value
            );
        }
    }
}
