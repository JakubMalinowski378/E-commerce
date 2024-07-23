using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
}
