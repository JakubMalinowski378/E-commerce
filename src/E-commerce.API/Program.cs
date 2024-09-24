using E_commerce.API.Middlewares;
using E_commerce.Application.Extensions;
using E_commerce.Domain.Interfaces;
using E_commerce.Infrastructure.Extensions;
using E_commerce.Infrastructure.Seeders;
using E_commerce.Infrastructure.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEnd",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
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
builder.Services.AddInfrastucture(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
var app = builder.Build();

var scope = app.Services.CreateScope();
var databaseMigrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
await databaseMigrator.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var seeder = scope.ServiceProvider.GetRequiredService<IEcommerceSeeder>();
    await seeder.Seed();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors("FrontEnd");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }