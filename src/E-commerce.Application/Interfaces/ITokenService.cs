using E_commerce.Domain.Entities;

namespace E_commerce.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
