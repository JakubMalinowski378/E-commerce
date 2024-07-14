using MediatR;

namespace E_commerce.Application.Products.Commands.CreateProductCommand;
public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Supplier { get; set; } = string.Empty;
    public List<Guid> ProductCategoriesIds { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
