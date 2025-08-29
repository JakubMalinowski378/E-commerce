using E_commerce.Application.Configuration;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Options;

namespace E_commerce.Application.Features.Products.Commands.DeleteProductCommand;

public class DeleteProductCommandHandler(
    IRepository<Product> productRepository,
    IBlobStorageRepository blobStorageRepository,
    IOptions<BlobStorageSettings> blobStorageSettings,
    IAuthorizationService authorizationService)
    : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());

        if (!await authorizationService.HasPermission(product, ResourceOperation.Delete))
            throw new ForbidException();

        productRepository.Remove(product);
        await blobStorageRepository.DeleteBlobRangeAsync(
            blobStorageSettings.Value.ContainerName,
            product.ProductImagesUrls);
    }
}
