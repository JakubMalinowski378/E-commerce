using E_commerce.Application.Intrfaces;
using E_commerce.Domain.Entities;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;


namespace E_commerce.Application.Services
{
    public class TokenService : ITokenService
    {
        public readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.Email)
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescription);
            return handler.WriteToken(token);
        }
    }
}
