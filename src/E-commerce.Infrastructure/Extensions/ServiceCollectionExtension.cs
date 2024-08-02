using E_commerce.Application.Interfaces;
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

namespace E_commerce.Infrastructure.Extensions;
public static class ServiceCollectionExtension
{
    public static void AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EcommerceDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("EcommerceDb")));

        services.AddScoped<IEcommerceSeeder, EcommerceSeeder>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IBlobStorageRepository, BlobStorageRepository>();
        services.AddTransient<IProductImageRepository, ProductImageRepository>();

        services.AddScoped<ICartItemAuthorizationService, CartItemAuthorizationService>();
        services.AddScoped<IProductAuthorizationService, ProductAuthorizationService>();
        services.AddScoped<IAddressAuthorizationService, AddressAuthorizationService>();
        services.AddTransient<IProductImageService, ProductImageService>();
        services.AddTransient<IEmailSender, EmailSender>();

        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
    }
}
