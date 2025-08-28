using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_commerce.Infrastructure.Repositories;

public class Repository<TEntity>(
    ECommerceDbContext dbContext)
    : IRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public async Task AddAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        => await _dbSet.AddRangeAsync(entities);

    public async Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        bool asNoTracking = false)
    {
        var query = _dbSet.AsQueryable();

        if (asNoTracking)
            _ = query.AsNoTracking();

        if (include is not null)
            _ = include(query);

        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        bool asNoTracking = false)
    {
        var query = _dbSet.AsQueryable();

        if (asNoTracking)
            query.AsNoTracking();

        if (include is not null)
            include(query);

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(
        object id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
        bool asNoTracking = false)
    {
        var keyProperty = typeof(TEntity).GetProperties()
            .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
        if (keyProperty == null)
            throw new InvalidOperationException($"Entity {typeof(TEntity).Name} does not have an 'Id' property.");

        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var property = Expression.Property(parameter, keyProperty);
        var idValue = Expression.Constant(Convert.ChangeType(id, keyProperty.PropertyType));
        var equal = Expression.Equal(property, idValue);
        var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);

        var query = _dbSet.AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        if (include is not null)
            query = include(query);

        return await query.FirstOrDefaultAsync(lambda);
    }

    public void Remove(TEntity entity)
        => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities)
        => _dbSet.RemoveRange(entities);
}
