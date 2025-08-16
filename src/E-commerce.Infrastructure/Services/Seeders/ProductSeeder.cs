using Bogus;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Services.Seeders;

internal class ProductSeeder(
    ECommerceDbContext dbContext)
    : ISeeder<Product>
{
    private const string locale = "pl";

    public async Task SeedAsync()
    {
        if (await dbContext.Products.AnyAsync())
            return;

        var salesmans = await dbContext.Users
            .Include(u => u.Role)
            .Where(u => u.Role.Name == UserRoles.Salesman)
            .ToListAsync();

        var categories = await dbContext.Categories.ToListAsync();

        Dictionary<string, object> additionalProperites = new()
        {
            { "number", 123 },
            { "double", 12.5 },
            { "boolean", true },
            { "string", "test"}
        };

        var faker = new Faker<Product>(locale)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.ProductImagesUrls, f => [f.Image.PicsumUrl()])
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 100))
            .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000))
            .RuleFor(p => p.IsHidden, f => f.Random.Bool())
            .RuleFor(p => p.AdditionalProperties, _ => additionalProperites)
            .RuleFor(p => p.Categories, f => [.. f.PickRandom(categories, 4)]);

        foreach (var salesman in salesmans)
        {
            var products = faker.Generate(10);
            foreach (var product in products)
            {
                product.UserId = salesman.Id;
            }
            await dbContext.Products.AddRangeAsync(products);
        }
        await dbContext.SaveChangesAsync();
    }
}
