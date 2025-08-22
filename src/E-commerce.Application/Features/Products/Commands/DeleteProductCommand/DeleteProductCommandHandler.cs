using E_commerce.Application.Configuration;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Options;

namespace E_commerce.Application.Features.Products.Commands.DeleteProductCommand;

public class DeleteProductCommandHandler(IProductRepository productRepository,
    IBlobStorageRepository blobStorageRepository,
    IOptions<BlobStorageSettings> blobStorageSettings)
    : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IBlobStorageRepository _blobStorageRepository = blobStorageRepository;
    private readonly IOptions<BlobStorageSettings> _blobStorageSettings = blobStorageSettings;

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());

        //if (!_productAuthorizationService.Authorize(product, ResourceOperation.Delete))
        //    throw new ForbidException();

        await _productRepository.DeleteAsync(product.Id);
        await _blobStorageRepository.DeleteBlobRangeAsync(
            _blobStorageSettings.Value.ContainerName,
            product.ProductImagesUrls);
    }
}
