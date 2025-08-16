using Bogus;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Services.Seeders;

internal class UserSeeder(
    ECommerceDbContext dbContext,
    IPasswordHasher passwordHasher)
    : ISeeder<User>
{
    private const string locale = "pl";
    public async Task SeedAsync()
    {
        if (await dbContext.Users.AnyAsync())
            return;

        const string password = "Password123!";
        var passwordHash = passwordHasher.Hash(password);
        var roles = await dbContext.Roles.ToListAsync();

        var faker = new Faker<User>(locale)
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.Gender, f => f.PickRandom(new List<char> { 'M', 'F' }))
            .RuleFor(u => u.DateOfBirth, f => f.Date.BetweenDateOnly(
                new DateOnly(1960, 1, 1), new DateOnly(2010, 12, 31)))
            .RuleFor(u => u.PhoneNumber, f => f.Person.Phone)
            .RuleFor(u => u.PasswordHash, _ => passwordHash)
            .RuleFor(u => u.EmailConfirmed, f => f.Random.Bool());

        var users = faker.Generate(roles.Count);

        for (int i = 0; i < users.Count; i++)
            users[i].Role = roles[i];

        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
    }
}
