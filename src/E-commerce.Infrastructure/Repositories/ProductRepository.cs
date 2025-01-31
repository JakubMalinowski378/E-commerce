using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace E_commerce.Infrastructure.Repositories;

public class ProductRepository(ProductsDbContext dbContext) : IProductRepository
{
    private readonly IMongoCollection<Product> _collection =
        dbContext.GetCollection<Product>(MongoCollections.Products);

    public async Task<Guid> Create(Product product)
    {
        await _collection.InsertOneAsync(product);
        return product.Id;
    }

    public async Task Delete(Product product)
    {
        await _collection.DeleteOneAsync(Builders<Product>.Filter.Eq("_id", product.Id));
    }

    public async Task DeleteUserProducts(Guid userId)
    {
        await _collection.DeleteManyAsync(Builders<Product>.Filter.Eq("userId", userId));
    }

    public async Task<(IEnumerable<Product>, int)> GetAllMatchingAsync(string? searchPhrase,
        int pageSize, int pageNumber)
    {
        var searchPhraseLower = searchPhrase?.Trim().ToLower();
        var filter = Builders<Product>.Filter.Empty;

        if (!string.IsNullOrEmpty(searchPhraseLower))
        {
            filter = Builders<Product>.Filter.Where(x => x.Name.ToLower().Contains(searchPhraseLower));
        }

        var count = await _collection.CountDocumentsAsync(filter);

        var products = await _collection.Find(filter)
            .Skip(pageSize * (pageNumber - 1))
            .Limit(pageSize)
            .ToListAsync();

        return (products, (int)count);
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
        => await _collection.Find(Builders<Product>.Filter.Eq("_id", id)).FirstOrDefaultAsync();

    public async Task<IEnumerable<Product>> GetUserProducts(Guid userId)
        => await _collection.Find(Builders<Product>.Filter.Eq("userId", userId)).ToListAsync();

    public async Task Update(Product product)
    {
        await _collection.ReplaceOneAsync(Builders<Product>.Filter.Eq("_id", product.Id), product);
    }
}
