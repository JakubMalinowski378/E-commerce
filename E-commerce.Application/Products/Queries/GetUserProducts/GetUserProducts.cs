using E_commerce.Application.Products.Dtos;
using MediatR;

namespace E_commerce.Application.Products.Queries.GetUserProducts;
public class GetUserProducts(Guid userId) : IRequest<IEnumerable<ProductDto>>
{
    public Guid UserId { get; set; } = userId;
}
