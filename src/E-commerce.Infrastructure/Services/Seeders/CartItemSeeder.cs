using Bogus;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Services.Seeders;

internal class CartItemSeeder(
    ECommerceDbContext dbContext)
    : ISeeder<CartItem>
{
    private const string locale = "pl";

    public async Task SeedAsync()
    {
        if (await dbContext.CartItems.AnyAsync())
            return;

        var userIds = await dbContext.Users
            .Include(u => u.Role)
            .Where(u => u.Role.Name == UserRoles.User)
            .Select(u => u.Id)
            .ToListAsync();

        var products = await dbContext.Products.ToListAsync();

        var faker = new Faker<CartItem>(locale)
            .RuleFor(ci => ci.Product, f => f.PickRandom(products))
            .RuleFor(ci => ci.ProductId, (f, ci) => ci.Product.Id)
            .RuleFor(ci => ci.UserId, f => f.PickRandom(userIds))
            .RuleFor(ci => ci.Quantity, (f, ci) => f.Random.Int(1, ci.Product.Quantity));

        var generatedCartItems = faker.Generate(30);

        var cartItems = generatedCartItems
            .GroupBy(ci => new { ci.UserId, ci.ProductId })
            .Select(g => g.First())
            .Take(10)
            .ToList();

        await dbContext.CartItems.AddRangeAsync(cartItems);
        await dbContext.SaveChangesAsync();
    }
}

