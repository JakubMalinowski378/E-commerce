using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

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

    [Fact()]
    public async Task Create()
    {
        // arrange
        Product product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Ratings = new List<Rating>(),
            Quantity = 1000,
            Price = 11.5M,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "NotNull",
        };

        //act
        await _productRepository.Create(product);
        var productFromDb = await _productRepository.GetProductByIdAsync(product.Id);

        //assert
        Assert.NotNull(productFromDb);
        Assert.Equal(product.Name, productFromDb.Name);

    }

    [Fact()]
    public async Task GetProductsAsync()
    {
        // arrange
        Product product1 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            Ratings = new List<Rating>(),
            Quantity = 1000,
            Price = 11.5M,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "NotNull",
        };
        Product product2 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test2",
            Ratings = new List<Rating>(),
            Quantity = 100,
            Price = 11,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "NotNull",
        };
        Product product3 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test3",
            Ratings = new List<Rating>(),
            Quantity = 0,
            Price = 5,
            IsHidden = true,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "Null",
        };

        //act
        await _productRepository.Create(product1);
        await _productRepository.Create(product2);
        await _productRepository.Create(product3);
        var productsFromDb = await _productRepository.GetProductsAsync();

        //assert
        Assert.Contains(productsFromDb, p => p.Name == product1.Name && p.Quantity == product1.Quantity && p.Price == product1.Price);
        Assert.Contains(productsFromDb, p => p.Name == product2.Name && p.Quantity == product2.Quantity && p.Price == product2.Price);
        Assert.Contains(productsFromDb, p => p.Name == product3.Name && p.Quantity == product3.Quantity && p.Price == product3.Price);

    }

    [Fact()]
    public async Task GetProductByIdAsync()
    {
        // arrange
        Product product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Ratings = new List<Rating>(),
            Quantity = 1000,
            Price = 11.5M,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "NotNull",
        };

        //act
        await _productRepository.Create(product);
        var productFromDb = await _productRepository.GetProductByIdAsync(product.Id);

        //assert
        Assert.NotNull(productFromDb);
        Assert.Equal(product.Name, productFromDb.Name);

    }

    [Fact()]
    public async Task Delete()
    {
        // arrange
        Product product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Ratings = new List<Rating>(),
            Quantity = 1000,
            Price = 11.5M,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "NotNull",
        };

        //act
        await _productRepository.Create(product);
        await _productRepository.Delete(product);
        var productFromDb = await _productRepository.GetProductByIdAsync(product.Id);

        //assert
        Assert.Null(productFromDb);
    }

    [Fact()]
    public async Task GetUserProducts()
    {
        // arrange
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Firstname = "Test"
        };
        Product product1 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            Ratings = new List<Rating>(),
            Quantity = 1000,
            Price = 11.5M,
            IsHidden = false,
            UserId = user.Id,
            AdditionalProperties = "Book",
        };
        Product product2 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test2",
            Ratings = new List<Rating>(),
            Quantity = 100,
            Price = 11,
            IsHidden = false,
            UserId = user.Id,
            AdditionalProperties = "Sports Book",
        };
        Product product3 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test3",
            Ratings = new List<Rating>(),
            Quantity = 0,
            Price = 5,
            IsHidden = true,
            UserId = user.Id,
            AdditionalProperties = "Null",
        };
        Product product4 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test4",
            Ratings = new List<Rating>(),
            Quantity = 20,
            Price = 5,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "Null",
        };

        //act
        await _productRepository.Create(product1);
        await _productRepository.Create(product2);
        await _productRepository.Create(product3);
        var productsFromDb = await _productRepository.GetUserProducts(user.Id);

        //assert
        Assert.Equal(3, productsFromDb.Count());
        Assert.Contains(productsFromDb, p => p.Name == product1.Name && p.Quantity == product1.Quantity && p.Price == product1.Price);
        Assert.Contains(productsFromDb, p => p.Name == product2.Name && p.Quantity == product2.Quantity && p.Price == product2.Price);
        Assert.Contains(productsFromDb, p => p.Name == product3.Name && p.Quantity == product3.Quantity && p.Price == product3.Price);
    }
    /*
    [Fact()]
    public async Task GetAllMatchingAsync()
    {
        // arrange
        Product product1 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            Ratings = new List<Rating>(),
            Quantity = 1000,
            Price = 11.5M,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "book",
        };
        Product product2 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test2",
            Ratings = new List<Rating>(),
            Quantity = 100,
            Price = 11,
            IsHidden = false,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "sports book",
        };
        Product product3 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test3",
            Ratings = new List<Rating>(),
            Quantity = 0,
            Price = 5,
            IsHidden = true,
            UserId = Guid.NewGuid(),
            AdditionalProperties = "Null",
        };

        //act
        await _productRepository.Create(product1);
        await _productRepository.Create(product2);
        await _productRepository.Create(product3);
        var productsFromDb = await _productRepository.GetAllMatchingAsync("Book",0,0);

        //assert
        Assert.Equal(productsFromDb.Item1.First().Name, product1.Name);
    }

    */

}
