using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace E_commerce.Infrastructure.Services;
public class ProductImageService(IBlobStorageRepository blobStorageRepository,
    IProductImageRepository productImageRepository)
    : IProductImageService
{
    private readonly IBlobStorageRepository _blobStorageRepository = blobStorageRepository;
    private readonly IProductImageRepository _productImageRepository = productImageRepository;

    public async Task HandleImageUploads(Product product, List<IFormFile> images)
    {
        var productImages = new ProductImage[images.Count];
        for (int i = 0; i < images.Count; i++)
        {
            var file = images[i];
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"{product.Id}-{i}{fileExtension}";
            productImages[i] = new()
            {
                FileName = fileName,
                ProductId = product.Id
            };
            await _blobStorageRepository.UploadBlobAsync(
                "test",
                fileName,
                file.ContentType,
                file.OpenReadStream());
        }
        await _productImageRepository.CreateRange(productImages);
    }
}
