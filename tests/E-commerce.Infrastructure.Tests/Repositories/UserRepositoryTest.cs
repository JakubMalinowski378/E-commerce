using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Tests.Repositories;

public class UserRepositoryTest
{
    private readonly DbContextOptions<EcommerceDbContext> _dbContextOptions;
    private readonly EcommerceDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IRolesRepository _rolesRepository;
    private readonly User _user = new()
    {
        Id = Guid.NewGuid(),
        FirstName = "Tester",
        LastName = "Test",
        Email = "email@gmail.com",
        Gender = 'M',
        DateOfBirth = new DateOnly(2002, 5, 10),
        PhoneNumber = "123456789"
    };

    public UserRepositoryTest()
    {
        _dbContextOptions = new DbContextOptionsBuilder<EcommerceDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;
        _context = new EcommerceDbContext(_dbContextOptions);
        _rolesRepository = new RolesRepository(_context);
        //_userRepository = new UserRepository(_context, _rolesRepository);
        _context.Roles.AddRangeAsync(new Role
        {
            Id = Guid.NewGuid(),
            Name = "User",
        }
);
        _context.SaveChanges();
    }

    [Fact()]
    public async Task Create()
    {
        // arrange

        //act
        await _userRepository.Create(_user);
        var userFromDb = await _userRepository.GetUserByIdAsync(_user.Id);

        //assert
        Assert.NotNull(userFromDb);
        Assert.Equal(_user.FirstName, userFromDb.FirstName);
    }

    [Fact()]
    public async Task GetUserByIdAsync()
    {
        // arrange

        //act
        await _userRepository.Create(_user);
        var userFromDb = await _userRepository.GetUserByIdAsync(_user.Id);

        //assert
        Assert.Equal(_user.FirstName, userFromDb?.FirstName);
    }

    [Fact()]
    public async Task GetUserByEmailAsync()
    {
        // arrange

        //act
        await _userRepository.Create(_user);
        var userFromDb = await _userRepository.GetUserByEmailAsync("email@gmail.com");

        //assert
        Assert.Equal(_user.FirstName, userFromDb?.FirstName);
    }

    [Fact()]
    public async Task GetUsersAsync()
    {
        // arrange
        User user2 = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Tester2",
            LastName = "Test2",
            Email = "email2@gmail.com",
            Gender = 'F',
            DateOfBirth = new DateOnly(2002, 5, 10),
            PhoneNumber = "322144443",
            EmailConfirmed = true,
        };

        //act
        await _userRepository.Create(_user);
        await _userRepository.Create(user2);
        var usersFromDb = await _userRepository.GetUsersAsync();

        //assert
        Assert.Contains(usersFromDb, u => u.FirstName == _user.FirstName && u.LastName == _user.LastName);
        Assert.Contains(usersFromDb, u => u.FirstName == user2.FirstName && u.LastName == user2.LastName);
    }

    [Fact()]
    public async Task UserExists()
    {
        // arrange

        //act

        //assert

    }

    [Fact()]
    public async Task DeleteUser()
    {
        // arrange

        //act

        //assert

    }

    [Fact()]
    public async Task ApplyIncludes()
    {
        // arrange

        //act

        //assert

    }

    [Fact()]
    public async Task GetAllRatingsOfUser()
    {
        // arrange

        //act

        //assert

    }

    [Fact()]
    public async Task GetUserByConfirmationTokenAsync()
    {
        // arrange

        //act

        //assert

    }

    [Fact()]
    public async Task GetUserByResetPasswordTokenAsync()
    {
        // arrange

        //act

        //assert

    }
}
