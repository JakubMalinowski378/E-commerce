using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_commerce.Infrastructure.Repositories;

public class ProductRepository(EcommerceDbContext dbContext) : IProductRepository
{
    private readonly EcommerceDbContext _dbContext = dbContext;

    public async Task<Guid> Create(Product product)
    {
        _dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        return product.Id;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
        => await _dbContext.Products.ToListAsync();

    public async Task<Product?> GetProductByIdAsync(Guid id, params Expression<Func<Product, object>>[] includePredicates)
    {
        var query = ApplyIncludes(includePredicates);
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    private IQueryable<Product> ApplyIncludes(params Expression<Func<Product, object>>[] includePredicates)
    {
        var query = _dbContext.Products.AsQueryable();
        foreach (var includePredicate in includePredicates)
            query = query.Include(includePredicate);
        return query;
    }
}
