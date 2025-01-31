namespace E_commerce.Application.Configuration;

public class BlobStorageSettings
{
    public string BlobStorageConnectionString { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

}
