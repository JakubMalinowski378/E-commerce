﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using E_commerce.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace E_commerce.Infrastructure.Repositories;
public class BlobStorageRepository(IConfiguration configuration) : IBlobStorageRepository
{
    private readonly string blobStorageConnectionString = configuration["BlobStorageSettings:BlobStorageConnectionString"]!;

    public async Task DeleteBlobAsync(string containerName, string blobName)
    {
        var blobServiceClient = new BlobServiceClient(blobStorageConnectionString);

        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteAsync();
    }

    public async Task DeleteBlobRangeAsync(string containerName, IEnumerable<string> blobNames)
    {
        var blobServiceClient = new BlobServiceClient(blobStorageConnectionString);

        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        foreach (var blobName in blobNames)
        {
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteAsync();
        }
    }

    public async Task UploadBlobAsync(string containerName, string blobName, string contentType, Stream content)
    {
        var blobServiceClient = new BlobServiceClient(blobStorageConnectionString);

        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();
        await containerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        var blobHttpHeaders = new BlobHttpHeaders()
        {
            ContentType = contentType
        };

        await blobClient.UploadAsync(content, blobHttpHeaders);
    }
}
