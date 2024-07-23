using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Products.Commands.DeleteProductCommand;
public class DeleteProductCommandHandler(IProductRepository productRepository,
    IProductAuthorizationService productAuthorizationService)
    : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductAuthorizationService _productAuthorizationService = productAuthorizationService;

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());

        if (!_productAuthorizationService.Authorize(product, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }

        await _productRepository.Delete(product);
    }
}
