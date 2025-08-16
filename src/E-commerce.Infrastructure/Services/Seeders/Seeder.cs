
using Bogus;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace E_commerce.Infrastructure.Services.Seeders;

internal class Seeder(
    IWebHostEnvironment webHostEnvironment,
    ISeeder<User> userSeeder,
    ISeeder<Permission> permissionSeeder,
    ISeeder<Role> roleSeeder,
    ISeeder<Address> addressSeeder,
    ISeeder<Category> categorySeeder,
    ISeeder<CartItem> cartItemSeeder,
    ISeeder<Product> productSeeder,
    ISeeder<Rating> ratingSeeder)
    : ISeeder
{
    public async Task SeedAsync()
    {
        Randomizer.Seed = new Random(1337);

        await permissionSeeder.SeedAsync();
        await roleSeeder.SeedAsync();
        await categorySeeder.SeedAsync();

        if (webHostEnvironment.IsDevelopment())
        {
            await userSeeder.SeedAsync();
            await addressSeeder.SeedAsync();
            await productSeeder.SeedAsync();
            await ratingSeeder.SeedAsync();
            await cartItemSeeder.SeedAsync();
        }
    }
}
