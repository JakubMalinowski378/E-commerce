using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace E_commerce.Infrastructure.Persistance;

public class EcommerceDbContextFactory : IDesignTimeDbContextFactory<EcommerceDbContext>
{
    public EcommerceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EcommerceDbContext>();

        var basePath = Directory.GetParent(Directory.GetCurrentDirectory()!)!.FullName;
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile(Path.Combine("E-commerce.API", "appsettings.json"))
            .Build();
        var connectionString = configuration.GetConnectionString("DockerDb");

        optionsBuilder.UseSqlServer(connectionString);

        return new EcommerceDbContext(optionsBuilder.Options);
    }
}
