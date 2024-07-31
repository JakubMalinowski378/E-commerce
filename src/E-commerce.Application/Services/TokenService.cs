using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_commerce.Application.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(config["TokenKey"] ?? throw new Exception("token key was not found")));

    public string CreateToken(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber)
        };

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescription = new SecurityTokenDescriptor
        {

            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials,

        };

        var handler = new JwtSecurityTokenHandler();
        try
        {
            var token = handler.CreateToken(tokenDescription);
            return handler.WriteToken(token);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as appropriate
            throw new InvalidOperationException("An error occurred while generating the token.", ex);
        }
    }
}