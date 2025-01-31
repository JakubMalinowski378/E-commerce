using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Products.Commands.CreateProductCommand;
public class CreateProductCommandHandler(IMapper mapper,
    IUserContext userContext,
    IProductRepository productRepository,
    IProductAuthorizationService productAuthorizationService,
    IProductImageService productImageService,
    IProductCategoryRepository productCategoryRepository)
    : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductAuthorizationService _productAuthorizationService = productAuthorizationService;
    private readonly IProductImageService _productImageService = productImageService;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUserContext _userContext = userContext;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        if (!_productAuthorizationService.Authorize(product, ResourceOperation.Create))
            throw new ForbidException();

        var user = _userContext.GetCurrentUser();
        product.UserId = user!.Id;
        await _productImageService.HandleImageUploads(product, request.Images);
        var productId = await _productRepository.Create(product);

        var productCategories = request.ProductCategoriesIds.Select(x => new ProductCategory()
        {
            CategoryId = x,
            ProductId = productId
        }).ToArray();

        await _productCategoryRepository.AddCategoriesToProduct(productCategories);

        return product.Id;
    }
}
