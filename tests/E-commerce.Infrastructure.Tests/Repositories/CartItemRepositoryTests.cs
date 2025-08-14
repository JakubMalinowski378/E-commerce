using Bogus;
using Bogus.DataSets;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


namespace E_commerce.Infrastructure.Tests.Repositories;

public class CartItemRepositoryTests 
{
    private readonly DbContextOptions<ECommerceDbContext> _dbContextOptions;
    private readonly ECommerceDbContext _context;
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ECommerceDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;
        _context = new ECommerceDbContext(_dbContextOptions);
        _cartItemRepository = new CartItemRepository(_context);
    }

    [Fact()]
    public async Task CreateCartItem_IsCreatedSuccessfully_ShouldBeEqual()
    {
        // arrange
        CartItem cartItem = new CartItem()
        {
            Id = Guid.NewGuid(),
            Quantity = 10,
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
        };

        //act
        await _cartItemRepository.CreateCartItem(cartItem);
        var cartItemFromDb = await _cartItemRepository.GetCartItemByIdAsync(cartItem.Id);

        //assert
        Assert.NotNull(cartItemFromDb);
        Assert.Equal(cartItemFromDb.Quantity, cartItem.Quantity);
    }
    [Fact()]
    public async Task DeleteCartItem_IsDeletedSuccessfully_ShouldBeNull()
    {
        // arrange
        CartItem cartItem = new CartItem()
        {
            Id = Guid.NewGuid(),
            Quantity = 10,
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
        };

        //act
        await _cartItemRepository.CreateCartItem(cartItem);
        await _cartItemRepository.DeleteCartItem(cartItem);
        var cartItemFromDb = await _cartItemRepository.GetCartItemByIdAsync(cartItem.Id);

        //assert
        Assert.Null(cartItemFromDb);
    }

    [Fact()]
    public async Task GetCartItemByIdAsync_IsGettingItemSuccessfully_ShouldBeEqual()
    {
        // arrange
        CartItem cartItem = new CartItem()
        {
            Id = Guid.NewGuid(),
            Quantity = 10,
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
        };

        //act
        await _cartItemRepository.CreateCartItem(cartItem);
        var cartItemFromDb = await _cartItemRepository.GetCartItemByIdAsync(cartItem.Id);

        //assert
        Assert.NotNull(cartItemFromDb);
        Assert.Equal(cartItemFromDb.Quantity, cartItem.Quantity);

    }

    [Fact()]
    public async Task GetUserCartItemsAsync_IsGettingUserItemsSuccessfully_ShouldContains()
    {
        // arrange
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Tester",
        };
        CartItem cartItem = new CartItem()
        {
            Id = Guid.NewGuid(),
            Quantity = 2,
            UserId = user.Id,
            ProductId = Guid.NewGuid(),
        };
        CartItem cartItem2 = new CartItem()
        {
            Id = Guid.NewGuid(),
            Quantity = 10,
            UserId = user.Id,
            ProductId = Guid.NewGuid(),
        };
        CartItem cartItem3 = new CartItem()
        {
            Id = Guid.NewGuid(),
            Quantity = 4,
            UserId = user.Id,
            ProductId = Guid.NewGuid(),
        };

        //act
        await _cartItemRepository.CreateCartItem(cartItem);
        await _cartItemRepository.CreateCartItem(cartItem2);
        await _cartItemRepository.CreateCartItem(cartItem3);
        var cartItemsFromDb = await _cartItemRepository.GetUserCartItemsAsync(user.Id);

        //assert
        Assert.Equal(3, cartItemsFromDb.Count());
        Assert.Contains(cartItemsFromDb, a => a.Quantity == cartItem.Quantity && a.UserId == cartItem.UserId);
        Assert.Contains(cartItemsFromDb, a => a.Quantity == cartItem2.Quantity && a.UserId == cartItem2.UserId);
        Assert.Contains(cartItemsFromDb, a => a.Quantity == cartItem3.Quantity && a.UserId == cartItem3.UserId);


    }


}
