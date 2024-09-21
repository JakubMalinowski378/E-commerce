using E_commerce.Domain.Interfaces;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Services;

public class DatabaseMigrator(EcommerceDbContext dbContext) : IDatabaseMigrator
{
    private readonly EcommerceDbContext _dbContext = dbContext;

    public async Task MigrateAsync()
    {
        if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}
