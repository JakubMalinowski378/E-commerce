using Xunit;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using Moq;
using E_commerce.Infrastructure.Persistance;
using Bogus;
using FluentAssertions;

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

    [Fact()]
    public void Create_WithMatchingAddedAdresses_ShouldReturnTrue()
    {

        // arrange
        Address address = new Address
        {
            Id = Guid.NewGuid(),
            StreetNumber = 4,
            ApartmentNumber = 15,
            PostalCode = "18-100",
            Street = "Glowna",
            City = "Lapy",
            UserId = Guid.NewGuid(),
        };

        //act
        _context.Addresses.Add(address);
        _context.SaveChanges();
        var addressFromDb = _context.Addresses.FirstOrDefault(a => a.Id == address.Id);
        addressFromDb.Should().NotBeNull();

        //assert
        addressFromDb.Should().Match<Address>(
            a =>  a.StreetNumber == address.StreetNumber &&
            a.ApartmentNumber == address.ApartmentNumber &&
            a.PostalCode == address.PostalCode &&
            a.Street == address.Street &&
            a.City == address.City);


    }

    [Fact()]
    public void Delete_WithCheckingIfNull_ShouldBeNull()
    {
        // arrange
        Address address = new Address
        {
            Id = Guid.NewGuid(),
            StreetNumber = 4,
            ApartmentNumber = 15,
            PostalCode = "18-100",
            Street = "Glowna",
            City = "Lapy",
            UserId = Guid.NewGuid(),
        };

        //act
        _context.Addresses.Add(address);
        _context.SaveChanges();
        Address? addressFromDb = _context.Addresses.FirstOrDefault();
        _context.Addresses.Remove(address);
        _context.SaveChanges();
        var addressFromDb2 = _context.Addresses.FirstOrDefault(a => a.Id == address.Id);

        //assert
        addressFromDb2.Should().BeNull();
    }

    [Fact()]
    public void GetByIdAsync_WithCheckingIfNotNull_ShouldBeNotNull()
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
        Address? addressFromDb =  _context.Addresses.FirstOrDefault(a => a.Id == address.Id);


        //assert
        addressFromDb.Should().NotBeNull();
    }

    [Fact()]
    public async void GetUserAddressesAsync_WithCheckingIfGettingAddressesByIdWork_ShouldReturnAssignableType()
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

        //act
        _context.Addresses.Add(address);
        _context.Addresses.Add(address2);
        _context.SaveChanges();
        var addressFromDb = await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();


        //assert
        addressFromDb.Should().BeAssignableTo<IEnumerable<Address>>();

    }
}

