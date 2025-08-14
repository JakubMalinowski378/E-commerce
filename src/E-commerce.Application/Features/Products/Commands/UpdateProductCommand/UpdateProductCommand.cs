using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace E_commerce.Application.Features.Products.Commands.UpdateProductCommand;

[ModelBinder(BinderType = typeof(UpdateProductCommandModelBinder))]
public class UpdateProductCommand : IRequest
{
    [SwaggerIgnore]
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<int> ProductCategoriesIds { get; set; } = [];
    public int Quantity { get; set; }
    public bool IsHidden { get; set; }
    public decimal Price { get; set; }
    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();
    public List<IFormFile> Images { get; set; } = default!;
}
