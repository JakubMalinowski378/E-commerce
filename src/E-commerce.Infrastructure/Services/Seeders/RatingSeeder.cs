using Bogus;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Services.Seeders;

internal class RatingSeeder(
    ECommerceDbContext dbContext)
    : ISeeder<Rating>
{
    private const string locale = "pl";
    public async Task SeedAsync()
    {
        if (await dbContext.Ratings.AnyAsync())
            return;

        var products = await dbContext.Products.ToListAsync();
        var users = await dbContext.Users
            .Include(u => u.Role)
            .Where(u => u.Role.Name == UserRoles.User)
            .ToListAsync();

        var faker = new Faker<Rating>(locale)
            .RuleFor(r => r.UserId, f => f.PickRandom(users).Id)
            .RuleFor(r => r.ProductId, f => f.PickRandom(products).Id)
            .RuleFor(r => r.Rate, f => f.PickRandom<Ratings>())
            .RuleFor(r => r.AddedDate, f => f.Date.Past(1).ToUniversalTime())
            .RuleFor(r => r.Comment, f => f.Lorem.Sentence(10, 20).OrNull(f, 0.5f));

        var generatedRatings = faker.Generate(10);

        var ratings = generatedRatings
            .GroupBy(ci => new { ci.UserId, ci.ProductId })
            .Select(g => g.First())
            .Take(10)
            .ToList();

        await dbContext.Ratings.AddRangeAsync(ratings);
        await dbContext.SaveChangesAsync();
    }
}
