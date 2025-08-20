using E_commerce.API.Middlewares;
using E_commerce.Domain.Interfaces;
using E_commerce.Infrastructure.Services;
using Microsoft.OpenApi.Models;

namespace E_commerce.API.Extensions;

public static class AplicationServiceExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy("AngularApp",
                builder => builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            config.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date"
            });
            config.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                    },
                    []
                }
            });
        });
        services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();
        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddMemoryCache();

        return services;
    }
}
