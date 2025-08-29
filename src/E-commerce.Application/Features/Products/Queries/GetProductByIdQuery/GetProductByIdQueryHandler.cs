using AutoMapper;
using E_commerce.Application.Features.Products.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Products.Queries.GetProductByIdQuery;

public class GetProductByIdQueryHandler(
    IRepository<Product> productsRepository,
    IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
{
    public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());
        var productDto = mapper.Map<ProductDetailsDto>(product);

        return productDto;
    }
}
