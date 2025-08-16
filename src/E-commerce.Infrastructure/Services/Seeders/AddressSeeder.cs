using Bogus;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Services.Seeders;

internal class AddressSeeder(
    ECommerceDbContext dbContext)
    : ISeeder<Address>
{
    private const string locale = "pl";

    public async Task SeedAsync()
    {
        if (await dbContext.Addresses.AnyAsync())
            return;

        var users = await dbContext.Users.ToListAsync();

        var faker = new Faker<Address>(locale)
            .RuleFor(a => a.StreetNumber, f => f.Random.Number(1, 100).ToString())
            .RuleFor(a => a.ApartmentNumber, f => f.Random.Number(1, 20).ToString().OrNull(f, 0.5f))
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
            .RuleFor(a => a.Street, f => f.Address.StreetName())
            .RuleFor(a => a.City, f => f.Address.City());

        foreach (var user in users)
        {
            var addresses = faker.Generate(Random.Shared.Next(0, 3));
            user.Addresses = addresses;
        }

        await dbContext.SaveChangesAsync();
    }
}
