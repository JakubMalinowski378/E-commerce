using AutoMapper;
using E_commerce.Application.Common;
using E_commerce.Application.Products.Dtos;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Products.Queries.GetAllProducts;
public class GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PagedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var (products, totalCount) = await _productRepository
            .GetAllMatchingAsync(request.SearchPhrase, request.PageSize, request.PageNumber, x => x.ProductImages);

        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        var result = new PagedResult<ProductDto>(productDtos, totalCount, request.PageSize, request.PageNumber);

        return result;
    }
}
