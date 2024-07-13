using E_commerce.Application.Products.Dtos;
using MediatR;

namespace E_commerce.Application.Products.Queries.GetProductByIdQuery;
public class GetProductByIdQuery(Guid productId) : IRequest<ProductDto>
{
    public Guid ProductId { get; set; } = productId;
}
