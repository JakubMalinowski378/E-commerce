using E_commerce.Domain.Entities;

namespace E_commerce.Application.Intrfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
