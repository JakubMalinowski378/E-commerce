using System.Security.Cryptography;

namespace E_commerce.Infrastructure.Helpers;

public static class ImageHelper
{
    public static string CalculateImageHash(Stream fileStream)
    {
        using var hmac = new HMACSHA256();
        var hash = hmac.ComputeHash(fileStream);
        return Convert.ToHexStringLower(hash);
    }
}
