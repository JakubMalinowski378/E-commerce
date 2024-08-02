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

    public UserRepositoryTest()
    {
        _dbContextOptions = new DbContextOptionsBuilder<EcommerceDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;
        _context = new EcommerceDbContext(_dbContextOptions);
        _rolesRepository = new RolesRepository(_context);
        _userRepository = new UserRepository(_context, _rolesRepository);
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
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Firstname = "Tester",
            LastName = "Test",
            Email = "email@gmail.com",
            Gender = 'M',
            DateOfBirth = new DateOnly(2002,5,10),
            PhoneNumber = "123456789",
            PasswordHash = new byte[] {1,2,3,4,5,6,7,8},
            PasswordSalt = new byte[] {9,8,7,6,5,4,3,2},

        };

        //act
        var id = await _userRepository.Create(user);
        var userFromDb = await _userRepository.GetUserByIdAsync(user.Id);

        //assert
        Assert.NotNull(id);
        Assert.NotNull(userFromDb);
        //Assert.Equal(userFromDb.Firstname, user.Firstname);
    }

    [Fact()]
    public async Task GetUserByIdAsync()
    {
        // arrange
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Firstname = "Tester",
            LastName = "Test",
            Email = "email@gmail.com",
            Gender = 'M',
            DateOfBirth = new DateOnly(2002, 5, 10),
            PhoneNumber = "123456789",
            PasswordHash = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 },
            PasswordSalt = new byte[] { 9, 8, 7, 6, 5, 4, 3, 2 },
            EmailConfirmed = true,
        };

        //act
        await _userRepository.Create(user);
        var userFromDb = await _userRepository.GetUserByIdAsync(user.Id);

        //assert
        Assert.Equal(userFromDb.Firstname, user.Firstname);
    }

    [Fact()]
    public async Task GetUserByEmailAsync()
    {
        // arrange
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Firstname = "Tester",
            LastName = "Test",
            Email = "email@gmail.com",
            Gender = 'M',
            DateOfBirth = new DateOnly(2002, 5, 10),
            PhoneNumber = "123456789",
            PasswordHash = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 },
            PasswordSalt = new byte[] { 9, 8, 7, 6, 5, 4, 3, 2 },

        };

        //act
        await _userRepository.Create(user);
        var userFromDb = await _userRepository.GetUserByEmailAsync("email@gmail.com");

        //assert
        Assert.Equal(userFromDb.Firstname, user.Firstname);
    }

    [Fact()]
    public async Task GetUsersAsync()
    {
        // arrange
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Firstname = "Tester",
            LastName = "Test",
            Email = "email@gmail.com",
            Gender = 'M',
            DateOfBirth = new DateOnly(2002, 5, 10),
            PhoneNumber = "123456789",
            PasswordHash = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 },
            PasswordSalt = new byte[] { 9, 8, 7, 6, 5, 4, 3, 2 },
            EmailConfirmed = true,
        };
        User user2 = new User()
        {
            Id = Guid.NewGuid(),
            Firstname = "Tester2",
            LastName = "Test2",
            Email = "email2@gmail.com",
            Gender = 'F',
            DateOfBirth = new DateOnly(2002, 5, 10),
            PhoneNumber = "322144443",
            PasswordHash = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 },
            PasswordSalt = new byte[] { 9, 8, 7, 6, 5, 4, 3, 2 },
            EmailConfirmed = true,
        };

        //act
        await _userRepository.Create(user);
        await _userRepository.Create(user2);
        var usersFromDb = await _userRepository.GetUsersAsync();

        //assert
        Assert.Contains(usersFromDb, u => u.Firstname == user.Firstname && u.LastName == user.Firstname);
        Assert.Contains(usersFromDb, u => u.Firstname == user2.Firstname && u.LastName == user2.Firstname);

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
