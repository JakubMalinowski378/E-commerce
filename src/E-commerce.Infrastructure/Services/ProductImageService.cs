using E_commerce.Application.Configuration;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace E_commerce.Infrastructure.Services;
public class ProductImageService(
    IBlobStorageRepository blobStorageRepository,
    IOptions<BlobStorageSettings> blobStorageSettings,
    IUnitOfWork unitOfWork)
    : IProductImageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettings.Value;

    public async Task HandleImageUploads(Product product, List<IFormFile> images)
    {
        var productImageFileNames = new List<string>(images.Count);
        foreach (var file in images)
        {
            var fileStream = file.OpenReadStream();
            var fileExtension = Path.GetExtension(file.FileName);
            var fileHash = ImageHelper.CalculateImageHash(fileStream);

            fileStream.Position = 0;
            var fileName = fileHash + fileExtension;

            await blobStorageRepository.UploadBlobAsync(
                _blobStorageSettings.ContainerName,
                fileName,
                file.ContentType,
                fileStream);
            productImageFileNames.Add(fileName);
        }
        product.ProductImagesUrls = productImageFileNames;

        await unitOfWork.SaveChangesAsync();
    }
}
