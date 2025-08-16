using E_commerce.API.Extensions;
using E_commerce.API.Middlewares;
using E_commerce.Application.Extensions;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Interfaces;
using E_commerce.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi(builder.Configuration);
builder.Services.AddInfrastucture(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();
var databaseMigrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
await databaseMigrator.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
    await seeder.SeedAsync();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
