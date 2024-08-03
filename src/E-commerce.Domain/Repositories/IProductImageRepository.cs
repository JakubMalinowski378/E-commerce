using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface IProductImageRepository
{
    Task CreateRange(IEnumerable<ProductImage> productImages);
    Task Delete(ProductImage productImage);
}
