using E_commerce.Application.Interfaces;
using E_commerce.Domain.Helpers;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Authorization;
using E_commerce.Infrastructure.Configuration;
using E_commerce.Infrastructure.Persistance;
using E_commerce.Infrastructure.Repositories;
using E_commerce.Infrastructure.Seeders;
using E_commerce.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace E_commerce.Infrastructure.Extensions;
public static class ServiceCollectionExtension
{
    public static void AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EcommerceDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("ECommerceLocal")));

        services.AddSingleton<ProductsDbContext>();

        BsonSerializer.RegisterSerializer(typeof(Guid), new GuidAsStringSerializer());

        services.AddScoped<IEcommerceSeeder, EcommerceSeeder>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddTransient<IBlobStorageRepository, BlobStorageRepository>();

        services.AddScoped<ICartItemAuthorizationService, CartItemAuthorizationService>();
        services.AddScoped<IProductAuthorizationService, ProductAuthorizationService>();
        services.AddScoped<IAddressAuthorizationService, AddressAuthorizationService>();
        services.AddTransient<IProductImageService, ProductImageService>();
        services.AddTransient<IEmailNotificationService, EmailNotificationService>();
        services.AddTransient<IEmailSender, EmailSender>();

        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
    }
}
