using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Application.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(config["Jwt:Key"]
        ?? throw new Exception("Key was not found")));

    public (string accessToken, string refreshToken) GenerateTokens(User user)
    {
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Role, user.Role.Name)
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescription = new SecurityTokenDescriptor
        {

            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials,
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescription);

        var accessToken = handler.WriteToken(token);
        var refreshToken = GenerateRefreshToken();

        return (accessToken, refreshToken);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
