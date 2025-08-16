using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Services.Seeders;

internal class RoleSeeder(
    ECommerceDbContext dbContext)
    : ISeeder<Role>
{
    public async Task SeedAsync()
    {
        if (await dbContext.Roles.AnyAsync())
            return;

        var permissions = await dbContext.Permissions.ToListAsync();
        var permissionsDictionary = permissions.ToDictionary(p => $"{p.Resource}:{p.Action}");

        List<Role> roles = [
            new Role
            {
                Name = UserRoles.User,
                Permisisons = [
                    permissionsDictionary["product:read"],
                    permissionsDictionary["product:read_hidden"],
                    permissionsDictionary["address:read_owns"],
                    permissionsDictionary["address:update_owns"],
                    permissionsDictionary["cartitem:create"],
                    permissionsDictionary["cartitem:read_owns"],
                    permissionsDictionary["cartitem:delete_owns"],
                    permissionsDictionary["rating:create"],
                    permissionsDictionary["rating:delete_owns"]
                ]
            },
            new Role{
                Name = UserRoles.Salesman,
                Permisisons = [
                    permissionsDictionary["product:create"],
                    permissionsDictionary["product:update"],
                    permissionsDictionary["product:delete"],
                    permissionsDictionary["category:create"],
                    permissionsDictionary["category:update"],
                    permissionsDictionary["category:delete"],
                    permissionsDictionary["rating:delete_owns"]
                ]
            },
            new Role
            {
                Name = UserRoles.Manager,
                Permisisons = [
                    permissionsDictionary["product:read"],
                    permissionsDictionary["product:update"],
                    permissionsDictionary["product:delete"],
                    permissionsDictionary["category:read"],
                    permissionsDictionary["category:update"],
                    permissionsDictionary["category:delete"],
                    permissionsDictionary["user:read"]
                ]
            },
            new Role
            {
                Name = UserRoles.Admin,
                Permisisons = permissions
            }
        ];

        await dbContext.Roles.AddRangeAsync(roles);
        await dbContext.SaveChangesAsync();
    }
}
