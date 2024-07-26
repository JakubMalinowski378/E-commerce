using Xunit;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using Moq;
using E_commerce.Infrastructure.Persistance;
using Bogus;

namespace E_commerce.Infrastructure.Repositories.Tests;
public class AddressRepositoryTest
{
    private DbContextOptions<EcommerceDbContext> _dbContextOptions;
    private EcommerceDbContext _context;
    private IAddressRepository _addressRepository;

    public AddressRepositoryTest()
    {
        _dbContextOptions = new DbContextOptionsBuilder<EcommerceDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;

        _context = new EcommerceDbContext(_dbContextOptions);
        _addressRepository = new AddressRepository(_context);
    }

    //Tutaj sprawdzanie poprawności bazy danych będzie
    [Fact()]
    public void CreateTest()
    {

        // arrange
        Address address = new Address
        {
            Id = new Guid(),
            StreetNumber = 4,
            ApartmentNumber = 15,
            PostalCode = "18-100",
            Street = "Glowna",
            City = "Lapy"
        };

        //act
        _context.Addresses.Add(address);
        _context.SaveChanges();


        //assert
        var addressFromDb = _context.Addresses.FirstOrDefault();
        Assert.NotNull(addressFromDb);
        Assert.Equal(address.City, addressFromDb.City);
        
    }

    [Fact()]
    public void DeleteTest()
    {
        // arrange
        Address address = new Address
        {
            Id = new Guid(),
            StreetNumber = 4,
            ApartmentNumber = 15,
            PostalCode = "18-100",
            Street = "Glowna",
            City = "Lapy"
        };
        _context.Addresses.Add(address);
        _context.SaveChanges();
        Address? addressFromDb = _context.Addresses.FirstOrDefault();

        //act
        _context.Addresses.Remove(address);
        _context.SaveChanges();

        //assert
        var addressFromDb2 = _context.Addresses.FirstOrDefault();
        Assert.Null(addressFromDb2);
    }

    [Fact()]
    public void GetByIdAsyncTest()
    {
        // arrange
        Address address = new Address
        {
            Id = new Guid(),
            StreetNumber = 4,
            ApartmentNumber = 15,
            PostalCode = "18-100",
            Street = "Glowna",
            City = "Lapy"
        };
        _context.Addresses.Add(address);
        _context.SaveChanges();

        //act
        Address? addressFromDb = _context.Addresses.FirstOrDefault(a => a.Id == address.Id);


        //assert
        Assert.NotNull(addressFromDb);
    }

    [Fact()]
    public void GetUserAddressesAsyncTest()
    {
        // arrange
        Guid userId = new Guid();
        Address address = new Address
        {
            Id = new Guid(),
            StreetNumber = 4,
            ApartmentNumber = 15,
            PostalCode = "18-100",
            Street = "Glowna",
            City = "Lapy",
            UserId = userId
        };
        Address address2 = new Address
        {
            Id = new Guid(),
            StreetNumber = 5,
            ApartmentNumber = 45,
            PostalCode = "10-012",
            Street = "Witosa",
            City = "Warszawa",
            UserId = userId
        };
        _context.Addresses.Add(address);
        _context.Addresses.Add(address2);
        _context.SaveChanges();

        //act
        //Task<IEnumerable<Address>> addressFromDb = _context.Addresses.Where(a => a.UserId == userId).ToListAsync();


        //assert
        //Assert.IsType<Task<IEnumerable<Address>>>(addressFromDb);

    }

    [Fact()]
    public void SaveChangesTest()
    {
        // arrange
        Address address = new Address
        {
            Id = new Guid(),
            StreetNumber = 4,
            ApartmentNumber = 15,
            PostalCode = "18-100",
            Street = "Glowna",
            City = "Lapy",
            UserId = new Guid(),
        };

        //act
        _context.Addresses.Add(address);
        _context.SaveChanges();

        //assert
        var addressFromDb = _context.Addresses.FirstOrDefault();
        Assert.NotNull(addressFromDb);
        Assert.Equal(address.City, addressFromDb.City);

    }
}

