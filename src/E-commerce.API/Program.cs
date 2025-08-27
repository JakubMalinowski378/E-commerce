using E_commerce.API.Extensions;
using E_commerce.API.Middlewares;
using E_commerce.Application.Extensions;
using E_commerce.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi();
builder.Services.AddInfrastucture(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.SeedAsync();
}

await app.ApplyMigrations();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
