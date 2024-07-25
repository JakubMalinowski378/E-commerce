using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Interfaces;
public interface IAddressAuthorizationService
{
    bool Authorize(Address address, ResourceOperation resourceOperation);
}
