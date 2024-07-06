using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface IAddressRepository
{
    Task Create(Address address);
    Task SaveChanges();
    Task Delete(Address address);
    Task<Address?> GetById(Guid id);
    Task<IEnumerable<Address>> GetUserAddresses(Guid userId);
}
