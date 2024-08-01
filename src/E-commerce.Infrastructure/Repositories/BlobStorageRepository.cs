using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using E_commerce.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace E_commerce.Infrastructure.Repositories;
public class BlobStorageRepository(IConfiguration configuration) : IBlobStorageRepository
{
    private readonly string blobStorageConnectionString = configuration["blobStorageConnectionString"]!;
    public async Task DeleteBlobAsync(string containerName, string blobName)
    {
        var blobServiceClient = new BlobServiceClient(blobStorageConnectionString);

        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteAsync();
    }

    public async Task UploadBlobAsync(string containerName, string blobName, string contentType, Stream content)
    {
        var blobServiceClient = new BlobServiceClient(blobStorageConnectionString);

        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        var blobHttpHeaders = new BlobHttpHeaders() { ContentType = contentType };

        await blobClient.UploadAsync(content, blobHttpHeaders);
    }
}
