using E_commerce.Application.Configuration;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace E_commerce.Infrastructure.Services;
public class ProductImageService(IBlobStorageRepository blobStorageRepository,
    IOptions<BlobStorageSettings> blobStorageSettings,
    IProductRepository productRepository)
    : IProductImageService
{
    private readonly IBlobStorageRepository _blobStorageRepository = blobStorageRepository;
    private readonly IOptions<BlobStorageSettings> _blobStorageSettings = blobStorageSettings;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task HandleImageUploads(Product product, List<IFormFile> images)
    {
        var productImageFileNames = new List<string>(images.Count);
        for (int i = 0; i < images.Count; i++)
        {
            var file = images[i];
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"{product.Id}-{i}{fileExtension}";

            await _blobStorageRepository.UploadBlobAsync(
                _blobStorageSettings.Value.ContainerName,
                fileName,
                file.ContentType,
                file.OpenReadStream());
        }
        product.ProductImages = productImageFileNames;

        await _productRepository.Update(product);
    }
}
