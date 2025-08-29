using AutoMapper;
using E_commerce.Application.Features.Products.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Products.Queries.GetUserProducts;

public class GetUserProductsHandler(
    IRepository<Product> productRepository,
    IMapper mapper)
    : IRequestHandler<GetUserProducts, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetUserProducts request, CancellationToken cancellationToken)
    {
        var products = await productRepository.FindAsync(p => p.UserId == request.UserId);

        var productDtos = mapper.Map<IEnumerable<ProductDto>>(products);
        return productDtos;
    }
}
