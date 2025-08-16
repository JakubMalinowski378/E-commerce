using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Services.Seeders;

internal class PermissionSeeder(
    ECommerceDbContext dbContext)
    : ISeeder<Permission>
{
    public async Task SeedAsync()
    {
        if (await dbContext.Permissions.AnyAsync())
            return;

        var dbPermissions = await dbContext.Permissions.ToListAsync();

        List<Permission> permisisons = [
            // Product
            new(){ Resource = "product", Action = ResourceOperation.Create },
            new(){ Resource = "product", Action = ResourceOperation.Read },
            new(){ Resource = "product", Action = ResourceOperation.Update },
            new(){ Resource = "product", Action = ResourceOperation.Delete },
            new(){ Resource = "product", Action = ResourceOperation.ReadHidden },
            new(){ Resource = "product", Action = ResourceOperation.UpdateOwns },
            new(){ Resource = "product", Action = ResourceOperation.DeleteOwns },
            
            // Address
            new(){ Resource = "address", Action = ResourceOperation.Create },
            new(){ Resource = "address", Action = ResourceOperation.Read },
            new(){ Resource = "address", Action = ResourceOperation.Update },
            new(){ Resource = "address", Action = ResourceOperation.Delete },
            new(){ Resource = "address", Action = ResourceOperation.ReadOwns },
            new(){ Resource = "address", Action = ResourceOperation.UpdateOwns },
            new(){ Resource = "address", Action = ResourceOperation.DeleteOwns },
            
            // CartItem
            new(){ Resource = "cartitem", Action = ResourceOperation.Create },
            new(){ Resource = "cartitem", Action = ResourceOperation.Read },
            new(){ Resource = "cartitem", Action = ResourceOperation.Update },
            new(){ Resource = "cartitem", Action = ResourceOperation.Delete },
            new(){ Resource = "cartitem", Action = ResourceOperation.ReadOwns },
            new(){ Resource = "cartitem", Action = ResourceOperation.UpdateOwns },
            new(){ Resource = "cartitem", Action = ResourceOperation.DeleteOwns },
            
            // Category
            new(){ Resource = "category", Action = ResourceOperation.Create },
            new(){ Resource = "category", Action = ResourceOperation.Read },
            new(){ Resource = "category", Action = ResourceOperation.Update },
            new(){ Resource = "category", Action = ResourceOperation.Delete },
            new(){ Resource = "category", Action = ResourceOperation.ReadOwns },
            new(){ Resource = "category", Action = ResourceOperation.UpdateOwns },
            new(){ Resource = "category", Action = ResourceOperation.DeleteOwns },
            
            // Rating
            new(){ Resource = "rating", Action = ResourceOperation.Create },
            new(){ Resource = "rating", Action = ResourceOperation.Read },
            new(){ Resource = "rating", Action = ResourceOperation.Update },
            new(){ Resource = "rating", Action = ResourceOperation.Delete },
            new(){ Resource = "rating", Action = ResourceOperation.ReadOwns },
            new(){ Resource = "rating", Action = ResourceOperation.UpdateOwns },
            new(){ Resource = "rating", Action = ResourceOperation.DeleteOwns },
            
            // User
            new(){ Resource = "user", Action = ResourceOperation.Create },
            new(){ Resource = "user", Action = ResourceOperation.Read },
            new(){ Resource = "user", Action = ResourceOperation.Update },
            new(){ Resource = "user", Action = ResourceOperation.Delete },
            new(){ Resource = "user", Action = ResourceOperation.ReadOwns },
            new(){ Resource = "user", Action = ResourceOperation.UpdateOwns },
            new(){ Resource = "user", Action = ResourceOperation.DeleteOwns }
        ];

        var missingDbPermissions = permisisons
            .Where(p => !dbPermissions.Any(dbp => dbp.Resource == p.Resource && dbp.Action == p.Action))
            .ToList();

        await dbContext.Permissions.AddRangeAsync(missingDbPermissions);
        await dbContext.SaveChangesAsync();
    }
}
