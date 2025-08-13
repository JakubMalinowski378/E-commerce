using E_commerce.Application.Configuration;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace E_commerce.Infrastructure.Services;
public class ProductImageService(
    IBlobStorageRepository blobStorageRepository,
    IOptions<BlobStorageSettings> blobStorageSettings,
    IProductRepository productRepository)
    : IProductImageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettings.Value;

    public async Task HandleImageUploads(Product product, List<IFormFile> images)
    {
        var productImageFileNames = new List<string>(images.Count);
        foreach (var file in images)
        {
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string randomString = Guid.NewGuid().ToString("N")[..6];
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"{timestamp}-{randomString}{fileExtension}";

            await blobStorageRepository.UploadBlobAsync(
                _blobStorageSettings.ContainerName,
                fileName,
                file.ContentType,
                file.OpenReadStream());
            productImageFileNames.Add(fileName);
        }
        product.ProductImagesUrls = productImageFileNames;

        await productRepository.Update(product);
    }
}
