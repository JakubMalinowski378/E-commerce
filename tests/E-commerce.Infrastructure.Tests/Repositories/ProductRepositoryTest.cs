using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Tests.Repositories;

public class ProductRepositoryTest
{
    private readonly DbContextOptions<EcommerceDbContext> _dbContextOptions;
    private readonly EcommerceDbContext _context;
    private readonly IProductRepository _productRepository;

    public ProductRepositoryTest()
    {
        _dbContextOptions = new DbContextOptionsBuilder<EcommerceDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;
        _context = new EcommerceDbContext(_dbContextOptions);
    }

    public static IEnumerable<Object[]> _products()
    {
        yield return new Object[]{ new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            Quantity = 1000,
            Price = 11.5M,
            UserId = Guid.NewGuid(),
            AdditionalProperties = new Dictionary<string, object>()
        } };
        yield return new Object[]{ new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test2",
            Quantity = 100,
            Price = 11,
            UserId = Guid.NewGuid(),
            AdditionalProperties = new Dictionary<string, object>()
        } };
        yield return new Object[]{ new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test3",
            Quantity = 0,
            Price = 5,
            UserId = Guid.NewGuid(),
            AdditionalProperties = new Dictionary<string, object>()
        } };
        yield return new Object[]{ new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test4",
            Quantity = 20,
            Price = 5,
            UserId = Guid.NewGuid(),
            AdditionalProperties = new Dictionary<string, object>()
        } };
    }

    [Theory]
    [MemberData(nameof(_products))]
    public async Task Create(Product product)
    {
        // arrange

        //act
        await _productRepository.Create(product);
        var productFromDb = await _productRepository.GetProductByIdAsync(product.Id);

        //assert
        Assert.NotNull(productFromDb);
        Assert.Equal(product.Name, productFromDb.Name);

    }

    [Theory]
    [MemberData(nameof(_products))]
    public async Task GetProductByIdAsync(Product product)
    {
        // arrange

        //act
        var id = await _productRepository.Create(product);
        var productFromDb = await _productRepository.GetProductByIdAsync(product.Id);

        //assert
        Assert.NotNull(productFromDb);
        Assert.Equal(product.Name, productFromDb.Name);
    }

    [Theory]
    [MemberData(nameof(_products))]
    public async Task Delete(Product product)
    {
        // arrange

        //act
        await _productRepository.Create(product);
        await _productRepository.Delete(product);
        var productFromDb = await _productRepository.GetProductByIdAsync(product.Id);

        //assert
        Assert.Null(productFromDb);
    }

    [Theory]
    [MemberData(nameof(_products))]
    public async Task GetUserProducts(Product product)
    {
        // arrange
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Test"
        };
        product.UserId = user.Id;

        //act
        await _productRepository.Create(product);
        var productsFromDb = await _productRepository.GetUserProducts(user.Id);

        //assert
        Assert.Contains(productsFromDb, p => p.Name == product.Name && p.Quantity == product.Quantity && p.Price == product.Price);
    }

    [Fact()]
    public async Task GetAllMatchingAsync()
    {
        // arrange


        //act


        //assert
    }
}
