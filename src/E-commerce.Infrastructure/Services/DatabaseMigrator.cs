using E_commerce.Domain.Interfaces;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Services;

public class DatabaseMigrator(ECommerceDbContext dbContext) : IDatabaseMigrator
{
    public async Task MigrateAsync()
    {
        if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}
