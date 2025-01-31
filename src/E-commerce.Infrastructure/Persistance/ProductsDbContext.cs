using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace E_commerce.Infrastructure.Persistance;
public class ProductsDbContext
{
    private readonly IMongoDatabase _database;

    public ProductsDbContext(IOptions<MongoDbSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        _database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
        => _database.GetCollection<T>(collectionName);
}
