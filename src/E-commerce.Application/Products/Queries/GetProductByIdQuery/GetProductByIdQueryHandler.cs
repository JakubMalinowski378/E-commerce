using AutoMapper;
using E_commerce.Application.Products.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Products.Queries.GetProductByIdQuery;
public class GetProductByIdQueryHandler(IProductRepository productsRepository,
    IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
{
    private readonly IProductRepository _productsRepository = productsRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetProductByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());
        var productDto = _mapper.Map<ProductDetailsDto>(product);
        return productDto;
    }
}
