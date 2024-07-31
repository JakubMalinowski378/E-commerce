using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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

    [Fact()]
    public async Task Create()
    {
        // arrange
        Category category = new Category()
        {
            Id = Guid.NewGuid(),
            CategoryName = "Woda",
        };

        //act
        await _categoryRepository.Create(category);
        var categoryFromDb = await _context.Categories.FirstOrDefaultAsync();

        //assert
        Assert.NotNull(categoryFromDb);
        Assert.Equal(category.CategoryName, categoryFromDb.CategoryName);
    }

    [Fact()]
    public async Task Delete()
    {
        // arrange
        Category category = new Category()
        {
            Id = Guid.NewGuid(),
            CategoryName = "Woda",
        };

        //act
        await _categoryRepository.Create(category);
        await _categoryRepository.Delete(category);
        var categoryFromDb = await _context.Categories.FirstOrDefaultAsync();

        //assert
        Assert.Null(categoryFromDb);
    }

    [Fact()]
    public async Task GetAllAsync()
    {
        // arrange
        Category category1 = new Category()
        {
            Id = Guid.NewGuid(),
            CategoryName = "Woda",
        };
        Category category2 = new Category()
        {
            Id = Guid.NewGuid(),
            CategoryName = "Food",
        };
        Category category3 = new Category()
        {
            Id = Guid.NewGuid(),
            CategoryName = "Sports",
        };


        //act
        await _categoryRepository.Create(category1);
        await _categoryRepository.Create(category2);
        await _categoryRepository.Create(category3);
        var categoryFromDb = await _categoryRepository.GetAllAsync();

        //assert
        Assert.Equal(3, categoryFromDb.Count());
        Assert.Contains(categoryFromDb, a => a.CategoryName == category1.CategoryName);
        Assert.Contains(categoryFromDb, a => a.CategoryName == category2.CategoryName);
        Assert.Contains(categoryFromDb, a => a.CategoryName == category3.CategoryName);

    }
}
