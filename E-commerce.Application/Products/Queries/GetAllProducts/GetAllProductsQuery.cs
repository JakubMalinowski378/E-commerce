using E_commerce.Application.Common;
using E_commerce.Application.Products.Dtos;
using MediatR;

namespace E_commerce.Application.Products.Queries.GetAllProducts;
public class GetAllProductsQuery : IRequest<PagedResult<ProductDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
