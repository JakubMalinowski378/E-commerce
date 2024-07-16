using MediatR;
using System.Text.Json.Serialization;

namespace E_commerce.Application.Products.Commands.UpdateProductCommand;
public class UpdateProductCommand : IRequest
{
    [JsonIgnore]
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Supplier { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
