using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Interfaces;
public interface IProductAuthorizationService
{
    bool Authorize(Product product, ResourceOperation resourceOperation);
}
