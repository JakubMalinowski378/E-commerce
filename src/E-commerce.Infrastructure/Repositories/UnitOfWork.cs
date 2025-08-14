using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;

namespace E_commerce.Infrastructure.Repositories;

internal class UnitOfWork(ECommerceDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await dbContext.SaveChangesAsync(cancellationToken);
}
