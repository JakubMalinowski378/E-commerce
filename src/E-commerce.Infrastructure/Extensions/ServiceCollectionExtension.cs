using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Configuration;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using E_commerce.Infrastructure.Services;
using E_commerce.Infrastructure.Services.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace E_commerce.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("ECommerceLocal"));
        dataSourceBuilder.EnableDynamicJson();
        var dataSource = dataSourceBuilder.Build();
        services.AddDbContext<ECommerceDbContext>(options => options.UseNpgsql(dataSource));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IBlobStorageRepository, BlobStorageRepository>();

        services.AddTransient<IProductImageService, ProductImageService>();
        services.AddTransient<IEmailNotificationService, EmailNotificationService>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddTransient<IAuthorizationService, AuthorizationService>();
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

        services.AddScoped<ISeeder, Seeder>();
        services.AddScoped<ISeeder<Address>, AddressSeeder>();
        services.AddScoped<ISeeder<CartItem>, CartItemSeeder>();
        services.AddScoped<ISeeder<Category>, CategorySeeder>();
        services.AddScoped<ISeeder<Permission>, PermissionSeeder>();
        services.AddScoped<ISeeder<Product>, ProductSeeder>();
        services.AddScoped<ISeeder<Rating>, RatingSeeder>();
        services.AddScoped<ISeeder<Role>, RoleSeeder>();
        services.AddScoped<ISeeder<User>, UserSeeder>();

        return services;
    }
}
