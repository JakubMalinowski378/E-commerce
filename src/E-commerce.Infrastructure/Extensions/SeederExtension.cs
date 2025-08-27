using E_commerce.Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace E_commerce.Infrastructure.Extensions;

public static class SeederExtension
{
    public static async Task SeedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var seeder = services.GetRequiredService<ISeeder>();

        await seeder.SeedAsync();
    }
}
