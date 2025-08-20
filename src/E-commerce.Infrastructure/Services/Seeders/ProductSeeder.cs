using Bogus;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Helpers;
using E_commerce.Infrastructure.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Services.Seeders;

internal class ProductSeeder(
    ECommerceDbContext dbContext,
    IHttpClientFactory httpClientFactory,
    IProductImageService productImageService)
    : ISeeder<Product>
{
    private const string locale = "pl";
    private readonly HttpClient httpClient = httpClientFactory.CreateClient();

    public async Task SeedAsync()
    {
        if (await dbContext.Products.AnyAsync())
            return;

        var salesmans = await dbContext.Users
            .Include(u => u.Role)
            .Where(u => u.Role.Name == UserRoles.Salesman)
            .ToListAsync();

        var categories = await dbContext.Categories.ToListAsync();

        Dictionary<string, object>[] additionalProperites = [
            new()
            {
                { "number", 123 },
                { "double", 12.5 },
                { "boolean", true },
                { "string", "test"}
            },
            new()
            {
                {"test", "testvalue"}
            }
        ];

        var faker = new Faker<Product>(locale)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.ProductImagesUrls, f => [.. Enumerable.Range(0, f.Random.Int(1, 2))
                .Select(_ => f.Image.PicsumUrl())])
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 100))
            .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000))
            .RuleFor(p => p.IsHidden, f => f.Random.Bool())
            .RuleFor(p => p.AdditionalProperties, f => f.PickRandom(additionalProperites))
            .RuleFor(p => p.Categories, f => [.. f.PickRandom(categories, 4)]);

        foreach (var salesman in salesmans)
        {
            var products = faker.Generate(5);

            foreach (var product in products)
            {
                List<IFormFile> formFiles = [];
                product.UserId = salesman.Id;
                foreach (var imageUrl in product.ProductImagesUrls)
                {
                    var finalUrl = await SeederHelper.GetFinalUrlAsync(imageUrl);
                    var fileBytes = await httpClient.GetByteArrayAsync(finalUrl);
                    var stream = new MemoryStream(fileBytes);
                    formFiles.Add(new FormFile(stream, 0, stream.Length, null!, SeederHelper.GetFileNameFromUrl(finalUrl))
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = SeederHelper.GetContentType(finalUrl)
                    });
                }
                await productImageService.HandleImageUploads(product, formFiles);
            }
            await dbContext.Products.AddRangeAsync(products);
        }
        await dbContext.SaveChangesAsync();
    }
}
