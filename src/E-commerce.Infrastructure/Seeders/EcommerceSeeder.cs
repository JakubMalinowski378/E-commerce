using Bogus;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Infrastructure.Seeders;
public class EcommerceSeeder(EcommerceDbContext dbContext,
    IWebHostEnvironment webHostEnvironment,
    IProductImageService productImageService,
    IProductRepository productRepository,
    ProductsDbContext productsDbContext)
    : IEcommerceSeeder
{
    private readonly EcommerceDbContext _dbContext = dbContext;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
    private readonly IProductImageService _productImageService = productImageService;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMongoCollection<Product> _productCollection =
     productsDbContext.GetCollection<Product>(MongoCollections.Products);
    private const string Locale = "pl";
    private const int RowCount = 5;

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

            if (!_dbContext.Categories.Any())
            {
                var productCategories = GetCategories();
                _dbContext.Categories.AddRange(productCategories);
                await _dbContext.SaveChangesAsync();
            }

            if (_webHostEnvironment.IsProduction())
                return;

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

            if (!(await _productRepository.GetAllMatchingAsync("", 15, 1)).Item1.Any())
            {
                var userIds = _dbContext.Users.Select(x => x.Id);
                var productCategories = _dbContext.Categories.ToArray();
                var products = GetProducts(RowCount * 3, userIds, productCategories);

                foreach (var product in products)
                {
                    await _productRepository.Create(product);
                }
                await _dbContext.SaveChangesAsync();
            }

            var filter = Builders<Product>.Filter.And(
                Builders<Product>.Filter.Exists(p => p.ProductImages, true),
                Builders<Product>.Filter.Ne(p => p.ProductImages, []));

            if ((await _productCollection.FindAsync(filter)).Any())
            {
                var products = await _productCollection.Find(Builders<Product>.Filter.Empty).ToListAsync();
                var seederHelper = new SeederHelper();
                using var httpClient = new HttpClient();
                foreach (var product in products)
                {
                    var finalUrl = await seederHelper.GetFinalUrlAsync("https://picsum.photos/1280/768");
                    var fileBytes = await httpClient.GetByteArrayAsync(finalUrl);
                    var stream = new MemoryStream(fileBytes);
                    IFormFile formFile = new FormFile(stream, 0, stream.Length, null!, seederHelper.GetFileNameFromUrl(finalUrl))
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = seederHelper.GetContentType(finalUrl)
                    };
                    await _productImageService.HandleImageUploads(product, [formFile]);
                }
            }

            if (!_dbContext.Ratings.Any())
            {
                var productIds = (await _productCollection.Find(Builders<Product>.Filter.Empty)
                    .ToListAsync())
                    .Select(x => x.Id);
                var userIds = _dbContext.Users.Select(x => x.Id).ToArray();
                var ratings = GetRatings(userIds, productIds);
                _dbContext.Ratings.AddRange(ratings);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.CartItems.Any())
            {
                var productIds = (await _productCollection.Find(Builders<Product>.Filter.Empty)
                    .ToListAsync())
                    .Select(x => x.Id);
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
        char[] availableGenders = ['F', 'M'];
        var users = new Faker<User>(Locale)
                .RuleFor(x => x.FirstName, y => y.Name.FirstName())
                .RuleFor(x => x.LastName, y => y.Name.LastName())
                .RuleFor(x => x.Email, y => y.Internet.Email())
                .RuleFor(x => x.Gender, y => y.PickRandom(availableGenders))
                .RuleFor(x => x.PhoneNumber, y => y.Person.Phone)
                .RuleFor(x => x.PasswordHash, _ => passwordHash)
                .RuleFor(x => x.PasswordSalt, _ => passwordSalt)
                .RuleFor(x => x.EmailConfirmed, y => y.Random.Bool())
                .RuleFor(x => x.DateOfBirth, y => y.Date.BetweenDateOnly(new DateOnly(1970, 1, 1), new DateOnly(2006, 12, 31)))
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
            .RuleFor(x => x.StreetNumber, y => y.Random.Int(1, 255).ToString())
            .RuleFor(x => x.ApartmentNumber, y => y.Random.Int(1, 100).ToString())
            .RuleFor(x => x.PostalCode, y => y.Address.ZipCode())
            .RuleFor(x => x.Street, y => y.Address.StreetName())
            .RuleFor(x => x.City, y => y.Address.City())
            .Generate(count);
        return addresses;
    }

    private static IEnumerable<Product> GetProducts(int count, IEnumerable<Guid> usersId,
        IEnumerable<Category> productCategories)
    {
        Dictionary<string, object>[] additionalProperties = [
            null!,
            new(){{"prop1", 20.5}},
            new(){{"prop1", "propvalue1"}, {"prop2", false}},
            new(){{"abc", "def"}, {"prop2", 234}, {"prop2", true}},
        ];

        var products = new Faker<Product>(Locale)
            .RuleFor(x => x.Name, y => y.Commerce.ProductName())
            .RuleFor(x => x.Quantity, y => y.Random.Int(0, 1000))
            .RuleFor(x => x.Price, y => y.Random.Decimal(1, 200))
            .RuleFor(x => x.UserId, y => y.PickRandom(usersId))
            .RuleFor(x => x.Categories, y => y.PickRandom(productCategories, Random.Shared.Next(1, 5)).ToList())
            .RuleFor(x => x.AdditionalProperties, y => y.PickRandom(additionalProperties))
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

    private static IEnumerable<Category> GetCategories()
    {
        List<string> categories = ["Electronics", "Clothing", "Home & Kitchen", "Beauty & Personal Care",
            "Sports & Outdoors", "Automotive", "Books", "Toys & Games", "Health & Household",
            "Grocery & Gourmet Food", "Office Products", "Pet Supplies", "Baby", "Tools & Home Improvement",
            "Garden & Outdoor", "Shoes", "Computers", "Video Games", "Musical Instruments",
            "Industrial", "Sports", "Jewelery", "Games", "Grocery", "Kids", "Music", "Outdoors", "Toys", "Tools"];
        return categories.Select(x => new Category() { CategoryName = x });
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
