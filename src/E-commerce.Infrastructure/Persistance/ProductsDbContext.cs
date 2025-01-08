using MongoDB.Driver;

namespace E_commerce.Infrastructure.Persistance;
public class ProductsDbContext
{
    private IMongoDatabase _database { get; set; }

    public ProductsDbContext(string connectionString, string databaseName)
    {
        var mongoClient = new MongoClient(connectionString);
        _database = mongoClient.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
        => _database.GetCollection<T>(collectionName);
}
