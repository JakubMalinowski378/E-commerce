using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Products.Commands.CreateProductCommand;
public class CreateProductCommandHandler(ICategoryRepository categoryRepository,
    IMapper mapper,
    IUserContext userContext,
    IProductRepository productRepository,
    IProductAuthorizationService productAuthorizationService)
    : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductAuthorizationService _productAuthorizationService = productAuthorizationService;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUserContext _userContext = userContext;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var categories = (await _categoryRepository.GetAllAsync())
            .Where(c => request.ProductCategoriesIds.Contains(c.Id)).ToList();

        var product = _mapper.Map<Product>(request);
        if (!_productAuthorizationService.Authorize(product, ResourceOperation.Create))
        {
            throw new ForbidException();
        }

        var user = _userContext.GetCurrentUser();
        product.UserId = user!.Id;
        product.Categories = categories;

        await _productRepository.Create(product);
        return product.Id;
    }
}
