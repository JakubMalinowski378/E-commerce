using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Tests.Repositories;

public class RatingRepositoryTests
{
    private readonly DbContextOptions<EcommerceDbContext> _dbContextOptions;
    private readonly EcommerceDbContext _context;
    private readonly IRatingRepository _ratingRepository;

    public RatingRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<EcommerceDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;
        _context = new EcommerceDbContext(_dbContextOptions);
        _ratingRepository = new RatingRepository(_context);
    }

    [Fact()]
    public async Task CreateRating()
    {
        // arrange
        Rating rating = new Rating()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            AddedDate = DateTime.Now,
            Rate = Domain.Constants.Ratings.VeryGood,
            Comment = "Masterpiece",
        };

        //act
        await _ratingRepository.CreateRating(rating);
        var ratingFromDb = await _ratingRepository.GetRatingById(rating.Id);

        //assert
        Assert.Equal(ratingFromDb.Comment, rating.Comment);
    }

    [Fact()]
    public async Task DeleteRating()
    {
        // arrange
        Rating rating = new Rating()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            AddedDate = DateTime.Now,
            Rate = Domain.Constants.Ratings.VeryGood,
            Comment = "Masterpiece",
        };

        //act
        await _ratingRepository.CreateRating(rating);
        await _ratingRepository.DeleteRating(rating);
        var ratingFromDb = await _ratingRepository.GetRatingById(rating.Id);

        //assert
        Assert.Null(ratingFromDb);
    }

    [Fact()]
    public async Task GetProductRatings()
    {
        // arrange
        Product product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
        };
        Rating rating = new Rating()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductId = product.Id,
            AddedDate = DateTime.Now,
            Rate = Domain.Constants.Ratings.VeryGood,
            Comment = "Masterpiece",
        };
        Rating rating2 = new Rating()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductId = product.Id,
            AddedDate = new DateTime(2022, 3, 5),
            Rate = Domain.Constants.Ratings.Good,
            Comment = "Kinda good",
        };
        //act
        await _ratingRepository.CreateRating(rating);
        await _ratingRepository.CreateRating(rating2);
        var ratingsFromDb = await _ratingRepository.GetProductRatings(product.Id);

        //assert
        Assert.Equal(2, ratingsFromDb.Count());
        Assert.Contains(ratingsFromDb, r => r.AddedDate == rating.AddedDate && r.Rate == rating.Rate && r.Comment == rating.Comment);
        Assert.Contains(ratingsFromDb, r => r.AddedDate == rating2.AddedDate && r.Rate == rating2.Rate && r.Comment == rating2.Comment);
    }

    [Fact()]
    public async Task GetRatingById()
    {
        // arrange
        Rating rating = new Rating()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            AddedDate = DateTime.Now,
            Rate = Domain.Constants.Ratings.VeryGood,
            Comment = "Masterpiece",
        };

        //act
        await _ratingRepository.CreateRating(rating);
        var ratingFromDb = await _ratingRepository.GetRatingById(rating.Id);

        //assert
        Assert.Equal(ratingFromDb.Comment, rating.Comment);
    }

    [Fact()]
    public async Task GetRatingByUserIdAndProductId()
    {
        // arrange
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Tester",
        };
        Product product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
        };
        Rating rating = new Rating()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            ProductId = product.Id,
            AddedDate = DateTime.Now,
            Rate = Domain.Constants.Ratings.VeryGood,
            Comment = "Masterpiece",
        };
        Rating rating2 = new Rating()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductId = product.Id,
            AddedDate = new DateTime(2022, 3, 5),
            Rate = Domain.Constants.Ratings.Good,
            Comment = "Kinda good",
        };

        //act
        await _ratingRepository.CreateRating(rating);
        await _ratingRepository.CreateRating(rating2);
        var ratingsFromDb = await _ratingRepository.GetRatingByUserIdAndProductId(user.Id, product.Id);

        //assert
        Assert.Equal(rating.AddedDate, ratingsFromDb?.AddedDate);
    }

    [Fact()]
    public async Task GetRatings()
    {
        // arrange
        Rating rating = new Rating()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            AddedDate = DateTime.Now,
            Rate = Domain.Constants.Ratings.VeryGood,
            Comment = "Masterpiece",
        };
        Rating rating2 = new Rating()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            AddedDate = new DateTime(2022, 3, 5),
            Rate = Domain.Constants.Ratings.Good,
            Comment = "Kinda good",
        };

        //act
        await _ratingRepository.CreateRating(rating);
        await _ratingRepository.CreateRating(rating2);
        var ratingsFromDb = await _ratingRepository.GetRatings();

        //assert
        Assert.Contains(ratingsFromDb, r => r.AddedDate == rating.AddedDate && r.Rate == rating.Rate && r.Comment == rating.Comment);
        Assert.Contains(ratingsFromDb, r => r.AddedDate == rating2.AddedDate && r.Rate == rating2.Rate && r.Comment == rating2.Comment);
    }
}
