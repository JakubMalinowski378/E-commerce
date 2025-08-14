using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace E_commerce.Application.Features.Products.Commands.CreateProductCommand;

[ModelBinder(BinderType = typeof(CreateProductCommandModelBinder))]
public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public List<int> ProductCategoriesIds { get; set; } = [];
    public int Quantity { get; set; }
    public bool IsHidden { get; set; }
    public decimal Price { get; set; }
    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();
    public List<IFormFile> Images { get; set; } = default!;
}
