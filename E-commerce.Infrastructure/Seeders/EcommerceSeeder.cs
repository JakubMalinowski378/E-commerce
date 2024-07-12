using Bogus;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Persistance;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Infrastructure.Seeders;
public class EcommerceSeeder(EcommerceDbContext dbContext) : IEcommerceSeeder
{
    private readonly EcommerceDbContext _dbContext = dbContext;
    private const string Locale = "pl";
    private const int RowCount = 10;

    public async Task Seed()
    {
        if (await _dbContext.Database.CanConnectAsync())
        {
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.ProductCategories.Any())
            {
                var productCategories = GetProductCategories();
                _dbContext.ProductCategories.AddRange(productCategories);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Users.Any())
            {
                var users = GetUsers(RowCount);
                _dbContext.Users.AddRange(users);

                var roles = _dbContext.Roles.ToArray();

                var addresses = GetAddresses(RowCount);
                for (int i = 0; i < RowCount; i++)
                {
                    addresses[i].UserId = users[i].Id;

                    var roleIndex = Random.Shared.Next(roles.Length);
                    users[i].Roles = [roles[roleIndex]];
                }

                _dbContext.Addresses.AddRange(addresses);

                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Products.Any())
            {
                var userIds = _dbContext.Users.Select(x => x.Id);
                var productCategories = _dbContext.ProductCategories.ToArray();
                var products = GetProducts(RowCount * 10, userIds, productCategories);
                _dbContext.Products.AddRange(products);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Ratings.Any())
            {
                var productIds = _dbContext.Products.Select(x => x.Id).ToArray();
                var userIds = _dbContext.Users.Select(x => x.Id).ToArray();
                var ratings = GetRatings(userIds, productIds);
                _dbContext.Ratings.AddRange(ratings);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.CartItems.Any())
            {
                var productIds = _dbContext.Products.Select(x => x.Id).ToArray();
                var userIds = _dbContext.Users.Select(x => x.Id).ToArray();
                var cartItems = GetCartItems(productIds, userIds);
                _dbContext.CartItems.AddRange(cartItems);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

    private static List<User> GetUsers(int count)
    {
        var (passwordHash, passwordSalt) = GeneratePassword("Password#123");

        var users = new Faker<User>(Locale)
                .RuleFor(x => x.Firstname, y => y.Name.FirstName())
                .RuleFor(x => x.LastName, y => y.Name.LastName())
                .RuleFor(x => x.Email, y => y.Internet.Email())
                .RuleFor(x => x.Gender, y => y.Random.Char('F', 'M'))
                .RuleFor(x => x.PhoneNumber, y => y.Person.Phone)
                .RuleFor(x => x.PasswordHash, _ => passwordHash)
                .RuleFor(x => x.PasswordSalt, _ => passwordSalt)
                .RuleFor(x => x.EmailConfirmed, y => y.Random.Bool())
                .Generate(count);
        return users;
    }

    private static (byte[] PasswordHash, byte[] PasswordSalt) GeneratePassword(string password)
    {
        using var hmac = new HMACSHA512();
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return (passwordHash, hmac.Key);
    }

    private static List<Address> GetAddresses(int count)
    {
        var addresses = new Faker<Address>(Locale)
            .RuleFor(x => x.StreetNumber, y => y.Random.Int(1, 255))
            .RuleFor(x => x.ApartmentNumber, y => y.Random.Int(1, 100))
            .RuleFor(x => x.PostalCode, y => y.Address.ZipCode())
            .RuleFor(x => x.Street, y => y.Address.StreetName())
            .RuleFor(x => x.City, y => y.Address.City())
            .Generate(count);
        return addresses;
    }

    private static IEnumerable<Product> GetProducts(int count, IEnumerable<Guid> usersId,
        IEnumerable<ProductCategory> productCategories)
    {
        var products = new Faker<Product>(Locale)
            .RuleFor(x => x.Name, y => y.Commerce.ProductName())
            .RuleFor(x => x.Supplier, y => y.Company.CompanyName())
            .RuleFor(x => x.Quantity, y => y.Random.Int(0, 1000))
            .RuleFor(x => x.Price, y => y.Random.Decimal(1, 200))
            .RuleFor(x => x.OwnerId, y => y.PickRandom(usersId))
            .RuleFor(x => x.ProductCategories, y => y.PickRandom(productCategories, Random.Shared.Next(1, 5)).ToList())
            .Generate(count);
        return products;
    }

    private static IEnumerable<CartItem> GetCartItems(IEnumerable<Guid> productIds, IEnumerable<Guid> userIds)
    {
        var cartItems = new Faker<CartItem>(Locale)
            .RuleFor(x => x.ProductId, y => y.PickRandom(productIds))
            .RuleFor(x => x.UserId, y => y.PickRandom(userIds))
            .RuleFor(x => x.Quantity, y => y.Random.Int(1, 80))
            .Generate(200);
        return cartItems;
    }

    private static IEnumerable<Rating> GetRatings(IEnumerable<Guid> userIds, IEnumerable<Guid> productIds)
    {
        var ratings = new Faker<Rating>(Locale)
            .RuleFor(x => x.AddedDate, y => y.Date.Between(
                new DateTime(2020, 1, 1), DateTime.UtcNow))
            .RuleFor(x => x.Rate, y => y.PickRandom<Ratings>())
            .RuleFor(x => x.Comment, y => y.Lorem.Text())
            .RuleFor(x => x.ProductId, y => y.PickRandom(productIds))
            .RuleFor(x => x.UserId, y => y.PickRandom(userIds))
            .Generate(100);
        return ratings;
    }

    private static IEnumerable<ProductCategory> GetProductCategories()
    {
        var faker = new Faker(Locale);
        var categories = faker.Commerce.Categories(50).ToHashSet();
        return categories.Select(x => new ProductCategory() { CategoryName = x });
    }

    private static List<Role> GetRoles()
    {
        return
        [
            new(){ Name = "User"},
            new(){ Name = "Salesman"},
            new(){ Name = "Manager"},
            new(){ Name = "Admin"}
        ];
    }
}
