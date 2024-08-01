namespace E_commerce.Domain.Repositories;
public interface IBlobStorageRepository
{
    Task UploadBlobAsync(string containerName, string blobName, string contentType, Stream content);
    Task DeleteBlobAsync(string containerName, string blobName);
}
