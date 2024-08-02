using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace E_commerce.Application.Products.Commands.DeleteProductCommand;
public class DeleteProductCommandHandler(IProductRepository productRepository,
    IProductAuthorizationService productAuthorizationService,
    IBlobStorageRepository blobStorageRepository,
    IConfiguration configuration)
    : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductAuthorizationService _productAuthorizationService = productAuthorizationService;
    private readonly IBlobStorageRepository _blobStorageRepository = blobStorageRepository;
    private readonly IConfiguration _configuration = configuration;

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductId, x => x.ProductImages)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());

        if (!_productAuthorizationService.Authorize(product, ResourceOperation.Delete))
            throw new ForbidException();

        await _productRepository.Delete(product);
        await _blobStorageRepository.DeleteBlobRangeAsync(
            _configuration["BlobStorage:ContainerName"]!,
            product.ProductImages.Select(x => x.FileName));
    }
}
