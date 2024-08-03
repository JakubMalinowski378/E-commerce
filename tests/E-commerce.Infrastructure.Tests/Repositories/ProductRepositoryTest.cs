using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace E_commerce.Infrastructure.Tests.Repositories;

public  class ProductRepositoryTest 
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
        _productRepository = new ProductRepository(_context);
    }

    public static IEnumerable<Object[]> _products() 
    {
        yield return new Object[]{ new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            Ratings = new List<Rating>(),
            Quantity = 1000,
            Price = 11.5M,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "NotNull",
        } };
        yield return new Object[]{ new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test2",
            Ratings = new List<Rating>(),
            Quantity = 100,
            Price = 11,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "NotNull1",
        } };
        yield return new Object[]{ new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test3",
            Ratings = new List<Rating>(),
            Quantity = 0,
            Price = 5,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "Null2",
        } };
        yield return new Object[]{ new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test4",
            Ratings = new List<Rating>(),
            Quantity = 20,
            Price = 5,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "Null3",
        } };
    }

    [Theory]
    [MemberData(nameof(_products))]
    public async Task Create(Product prod)
    {
        // arrange
        Product product = prod;

        //act
        await _productRepository.Create(product);
        var productFromDb = await _productRepository.GetProductByIdAsync(product.Id);

        //assert
        Assert.NotNull(productFromDb);
        Assert.Equal(product.Name, productFromDb.Name);

    }

    [Theory]
    [MemberData(nameof(_products))]
    public async Task GetProductsAsync(Product prod)
    {
        // arrange
        Product product1 = prod;

        //act
        await _productRepository.Create(product1);
        var productsFromDb = await _productRepository.GetProductsAsync();

        //assert
        Assert.Contains(productsFromDb, p => p.Name == product1.Name && p.Quantity == product1.Quantity && p.Price == product1.Price);

    }

    [Theory]
    [MemberData(nameof(_products))]
    public async Task GetProductByIdAsync(Product prod)
    {
        // arrange
        Product product = prod;

        //act
        await _productRepository.Create(product);
        var productFromDb = await _productRepository.GetProductByIdAsync(product.Id);

        //assert
        Assert.NotNull(productFromDb);
        Assert.Equal(product.Name, productFromDb.Name);

    }

    [Theory]
    [MemberData(nameof(_products))]
    public async Task Delete(Product prod)
    {
        // arrange
        Product product = prod;

        //act
        await _productRepository.Create(product);
        await _productRepository.Delete(product);
        var productFromDb = await _productRepository.GetProductByIdAsync(product.Id);

        //assert
        Assert.Null(productFromDb);
    }

    [Theory]
    [MemberData(nameof(_products))]
    public async Task GetUserProducts(Product prod)
    {
        // arrange
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Firstname = "Test"
        };
        Product product = prod;
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
