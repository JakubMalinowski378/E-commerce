using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Products.Commands.CreateProductCommand;
public class CreateProductCommandHandler(IProductCategoryRepository productCategoryRepository,
    IMapper mapper,
    IUserContext userContext,
    IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUserContext _userContext = userContext;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var categories = (await _productCategoryRepository.GetAllAsync())
            .Where(c => request.ProductCategoriesIds.Contains(c.Id)).ToList();

        var product = _mapper.Map<Product>(request);

        var user = _userContext.GetCurrentUser();
        product.OwnerId = user!.Id;
        product.ProductCategories = categories;

        await _productRepository.Create(product);
        return product.Id;
    }
}
