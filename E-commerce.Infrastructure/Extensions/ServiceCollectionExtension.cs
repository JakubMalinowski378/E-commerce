using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_commerce.Infrastructure.Extensions;
public static class ServiceCollectionExtension
{
    public static void AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EcommerceDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("EcommerceDb")));

        services.AddScoped<IUserRepository, UserRepository>();
    }
}
