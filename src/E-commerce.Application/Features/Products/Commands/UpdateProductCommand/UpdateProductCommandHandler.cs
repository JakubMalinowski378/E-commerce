using AutoMapper;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Products.Commands.UpdateProductCommand;
public class UpdateProductCommandHandler(IProductRepository productRepository,
    IProductAuthorizationService productAuthorizationService,
    IMapper mapper)
    : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductAuthorizationService _productAuthorizationService = productAuthorizationService;
    private readonly IMapper _mapper = mapper;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());

        if (!_productAuthorizationService.Authorize(product, ResourceOperation.Update))
        {
            throw new ForbidException();
        }

        _mapper.Map(request, product);
        await _productRepository.Update(product);
    }
}
