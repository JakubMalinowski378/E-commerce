using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Tests.Repositories;
public class AddressRepositoryTests
{
    private readonly DbContextOptions<ECommerceDbContext> _dbContextOptions;
    private readonly ECommerceDbContext _context;
    private readonly IAddressRepository _addressRepository;
    private readonly Address _address = new()
    {
        StreetNumber = "4",
        ApartmentNumber = null,
        PostalCode = "18-100",
        Street = "Glowna",
        City = "Lapy"
    };

    public AddressRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ECommerceDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;
        _context = new ECommerceDbContext(_dbContextOptions);
        _addressRepository = new AddressRepository(_context);
    }

    [Fact()]
    public async Task Create_AddressIsCreatedSuccessfully_ShouldPass()
    {
        //act
        await _addressRepository.Create(_address);
        var addressFromDb = await _addressRepository.GetByIdAsync(_address.Id);

        //assert
        Assert.NotNull(addressFromDb);
        Assert.Equal(_address.City, addressFromDb.City);
    }

    [Fact()]
    public async Task Delete_AddressIsDeletedSuccessfully_ShouldPass()
    {
        //act
        await _addressRepository.Create(_address);
        await _addressRepository.Delete(_address);
        var addressFromDb = await _addressRepository.GetByIdAsync(_address.Id);

        //assert
        Assert.Null(addressFromDb);
    }

    [Fact()]
    public async Task GetByIdAsync()
    {
        //act
        await _addressRepository.Create(_address);
        var addressFromDb = await _addressRepository.GetByIdAsync(_address.Id);

        //assert
        Assert.NotNull(addressFromDb);
        Assert.Equal(addressFromDb.City, addressFromDb.City);
    }

    [Fact()]
    public async Task GetByIdAsync_AddressIsRetrievedSuccessfully_ShouldPass()
    {
        // arrange
        var userId = Guid.NewGuid();
        var _address = new Address
        {
            UserId = userId,
            StreetNumber = "4",
            ApartmentNumber = "15",
            PostalCode = "18-100",
            Street = "Glowna",
            City = "Lapy",
        };
        var address2 = new Address
        {
            UserId = userId,
            StreetNumber = "5",
            ApartmentNumber = "45",
            PostalCode = "10-012",
            Street = "Witosa",
            City = "Warszawa",
        };

        //act
        await _addressRepository.Create(_address);
        await _addressRepository.Create(address2);
        var addressesFromDb = await _addressRepository.GetUserAddressesAsync(userId);

        //assert
        Assert.Equal(2, addressesFromDb.Count());
        Assert.Contains(addressesFromDb, a => a.Street == "Glowna" && a.City == "Lapy" && a.PostalCode == "18-100");
        Assert.Contains(addressesFromDb, a => a.Street == "Witosa" && a.City == "Warszawa" && a.PostalCode == "10-012");
    }
}