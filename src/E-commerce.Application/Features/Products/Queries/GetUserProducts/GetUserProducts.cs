using E_commerce.Application.Features.Products.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Products.Queries.GetUserProducts;
public class GetUserProducts(Guid userId) : IRequest<IEnumerable<ProductDto>>
{
    public Guid UserId { get; set; } = userId;
}
