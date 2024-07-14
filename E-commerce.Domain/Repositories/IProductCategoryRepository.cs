using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface IProductCategoryRepository
{
    Task<IEnumerable<ProductCategory>> GetAllAsync();
}
