using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Tests.Repositories;

public class CategoryRepositoryTests
{
    private readonly DbContextOptions<EcommerceDbContext> _dbContextOptions;
    private readonly EcommerceDbContext _context;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<EcommerceDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;
        _context = new EcommerceDbContext(_dbContextOptions);
        _categoryRepository = new CategoryRepository(_context);
    }

    [Theory]
    [InlineData("Woda")]
    [InlineData("Food")]
    [InlineData("Sports")]
    public async Task GetAllAsyncTest(string categoryName)
    {
        // arrange
        Category category = new Category()
        {
            CategoryName = categoryName,
        };

        //act
        await _categoryRepository.Create(category);
        var categoryFromDb = await _categoryRepository.GetAllAsync();

        //assert
        Assert.Contains(categoryFromDb, a => a.CategoryName == category.CategoryName);
    }
}
