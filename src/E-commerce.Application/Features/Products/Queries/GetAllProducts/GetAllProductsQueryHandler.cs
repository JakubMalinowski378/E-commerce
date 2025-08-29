using AutoMapper;
using E_commerce.Application.Common;
using E_commerce.Application.Features.Products.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(
    IRepository<Product> productRepository,
    IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
{
    public async Task<PagedResult<ProductDto>> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var (products, totalCount) = await productRepository
            .GetPagedAsync(pageNumber: request.PageNumber,
                           pageSize: request.PageSize,
                           filter: request.SearchPhrase != null ? p => p.Name.Contains(request.SearchPhrase,
                                        StringComparison.InvariantCultureIgnoreCase) : null,
                           include: null,
                           orderBy: q => q.OrderBy(p => p.Name),
                           asNoTracking: true);

        var productDtos = mapper.Map<IEnumerable<ProductDto>>(products);

        var result = new PagedResult<ProductDto>(productDtos, totalCount, request.PageSize, request.PageNumber);

        return result;
    }
}
