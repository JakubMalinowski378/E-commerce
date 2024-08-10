using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Tests.Repositories;

public class RolesRepositoryTest
{
    private readonly DbContextOptions<EcommerceDbContext> _dbContextOptions;
    private readonly EcommerceDbContext _context;
    private readonly IRolesRepository _rolesRepository;

    public RolesRepositoryTest()
    {
        _dbContextOptions = new DbContextOptionsBuilder<EcommerceDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;
        _context = new EcommerceDbContext(_dbContextOptions);
        _rolesRepository = new RolesRepository(_context);
    }

    [Fact()]
    public async Task GetRole()
    {
        // arrange
        await _context.Roles.AddAsync(new Role
        {
            Id = Guid.NewGuid(),
            Name = "Test",
        }
        );
        _context.SaveChanges();

        //act
        var roleFromDb = await _rolesRepository.GetRole("Test");

        //assert
        Assert.Equal("Test", roleFromDb?.Name);
    }
}
