using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface IProductCategoryRepository
{
    Task AddCategoryToProduct(ProductCategory productCategory);
    Task AddCategoriesToProduct(IEnumerable<ProductCategory> productCategories);
    Task RemoveCategoryFromProduct(ProductCategory productCategory);
    Task RemoveCategoriesFromProduct(IEnumerable<ProductCategory> productCategories);
    Task<IEnumerable<ProductCategory>> GetProductCategories(Guid productId);
    Task SaveChangesAsync();
}
