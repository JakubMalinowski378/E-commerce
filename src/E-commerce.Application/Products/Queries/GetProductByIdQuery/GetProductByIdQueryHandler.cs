using AutoMapper;
using E_commerce.Application.Products.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Products.Queries.GetProductByIdQuery;
public class GetProductByIdQueryHandler(IProductRepository productsRepository,
    IMapper mapper,
    IProductCategoryRepository productCategoryRepository)
    : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
{
    private readonly IProductRepository _productsRepository = productsRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetProductByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());
        var productDto = _mapper.Map<ProductDetailsDto>(product);
        var productCategories = await _productCategoryRepository.GetProductCategories(product.Id);
        productDto.ProductCategories = productCategories.Select(x => x.Category.CategoryName).ToList();

        return productDto;
    }
}
