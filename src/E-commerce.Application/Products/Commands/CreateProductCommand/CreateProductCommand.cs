using MediatR;
using Microsoft.AspNetCore.Http;

namespace E_commerce.Application.Products.Commands.CreateProductCommand;
public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public List<Guid> ProductCategoriesIds { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string AdditionalProperties { get; set; } = string.Empty;
    public List<IFormFile> Images { get; set; } = default!;
}
