namespace E_commerce.Infrastructure.Seeders;
internal class SeederHelper
{
    public string GetContentType(string url)
    {
        var extension = Path.GetExtension(url);
        extension = extension[..extension.IndexOf('?')];
        return extension.
            ToLower() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            _ => "application/octet-stream"
        };
    }

    public async Task<string> GetFinalUrlAsync(string url)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        if ((int)response.StatusCode == 200)
            return response!.RequestMessage!.RequestUri!.AbsoluteUri;

        return url;
    }

    public string GetFileNameFromUrl(string url)
    {
        return Path.GetFileName(new Uri(url).AbsolutePath);
    }
}
