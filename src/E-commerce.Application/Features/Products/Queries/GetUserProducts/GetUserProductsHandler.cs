using AutoMapper;
using E_commerce.Application.Features.Products.Dtos;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Products.Queries.GetUserProducts;
public class GetUserProductsHandler(IProductRepository productRepository,
    IMapper mapper)
    : IRequestHandler<GetUserProducts, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ProductDto>> Handle(GetUserProducts request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetUserProductsAsync(request.UserId);
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return productDtos;
    }
}
