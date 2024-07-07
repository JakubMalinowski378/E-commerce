using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories
{
    public  interface IProductsRepository
    {
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Guid> Create(Product product);
    }
}
