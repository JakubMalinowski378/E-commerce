using E_commerce.Application.Features.Products.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Products.Queries.GetProductByIdQuery;
public class GetProductByIdQuery(Guid productId) : IRequest<ProductDetailsDto>
{
    public Guid ProductId { get; set; } = productId;
}
