using E_commerce.Application.Interfaces;
using E_commerce.Application.Users.Commands.RegisterUser;
using E_commerce.Application.Users.Queries.LoginUser;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace E_commerce.API.Tests.Controllers;
public class AccountControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IDatabaseMigrator> _databaseMigrator = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();
    private readonly LoginUserQuery _loginUserQuery = new()
    {
        Email = "test@example.com",
        Password = "Password#123"
    };
    private readonly RegisterUserCommand _registerUserCommand = new()
    {
        DateOfBirth = new DateOnly(2000, 1, 1),
        Email = "test@example.com",
        Password = "Password#123",
        Firstname = "Jan",
        LastName = "Nowak",
        Gender = 'M',
        PhoneNumber = "555555555"
    };

    public AccountControllerTests(WebApplicationFactory<Program> factory)
    {
        _databaseMigrator.Setup(m => m.MigrateAsync()).Returns(Task.CompletedTask);
        _tokenServiceMock.Setup(m => m.CreateToken(new User())).Returns("");
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.Replace(ServiceDescriptor.Scoped(typeof(IUserRepository),
                    _ => _userRepositoryMock.Object));

                services.Replace(ServiceDescriptor.Scoped(typeof(IDatabaseMigrator),
                    _ => _databaseMigrator.Object));

                services.Replace(ServiceDescriptor.Scoped(typeof(ITokenService),
                    _ => _tokenServiceMock.Object));
            });
        });
    }

    [Fact]
    public async Task Login_ForNonExisitingUser_ShouldReturn401Unauthorized()
    {
        _userRepositoryMock.Setup(m => m.GetUserByEmailAsync(_loginUserQuery.Email))
            .ReturnsAsync((User?)null);

        var client = _factory.CreateClient();
        var json = JsonSerializer.Serialize(_loginUserQuery);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/Account/login/", content);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_ForExisitingUser_ShouldReturn200Ok()
    {
        _userRepositoryMock.Setup(m => m.GetUserByEmailAsync(_loginUserQuery.Email, u => u.Roles))
            .ReturnsAsync(new User()
            {
                PasswordHash = [21, 176, 132, 76, 127, 71, 254, 53, 206, 177, 163, 29, 78, 158, 54, 169, 132, 159, 228, 166, 85, 15, 22, 245, 95, 140, 250, 39, 168, 150, 134, 223, 196, 246, 33, 140, 218, 234, 170, 62, 176, 253, 76, 236, 193, 220, 109, 5, 173, 12, 95, 154, 49, 183, 70, 142, 118, 98, 250, 90, 155, 149, 213, 184],
                PasswordSalt = [200, 192, 142, 150, 187, 30, 165, 78, 98, 85, 47, 45, 234, 25, 65, 144, 170, 194, 188, 216, 232, 189, 204, 118, 0, 60, 238, 144, 3, 61, 118, 129, 18, 88, 133, 164, 46, 129, 36, 158, 219, 187, 167, 161, 133, 146, 160, 140, 77, 102, 92, 104, 225, 198, 160, 41, 62, 200, 227, 35, 177, 166, 55, 222, 104, 2, 72, 160, 132, 181, 109, 94, 128, 151, 80, 36, 86, 121, 220, 169, 230, 182, 91, 57, 184, 46, 88, 152, 54, 208, 237, 7, 252, 76, 44, 110, 187, 247, 55, 236, 75, 28, 153, 73, 10, 35, 189, 121, 160, 25, 156, 207, 53, 70, 85, 160, 158, 86, 26, 28, 46, 117, 21, 50, 172, 229, 253, 136]
            });
        var client = _factory.CreateClient();
        var json = JsonSerializer.Serialize(_loginUserQuery);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/Account/login/", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Register_WhenEmailIsAlreadyInUse_ShouldReturn409Conflict()
    {
        _userRepositoryMock.Setup(m => m.UserExists(_registerUserCommand.Email)).ReturnsAsync(true);

        var client = _factory.CreateClient();
        var json = JsonSerializer.Serialize(_registerUserCommand);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/Account/register/", content);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Register_WhenGivenValidData_ShouldReturn200Ok()
    {
        _userRepositoryMock.Setup(m => m.UserExists(_registerUserCommand.Email)).ReturnsAsync(false);

        var client = _factory.CreateClient();
        var json = JsonSerializer.Serialize(_registerUserCommand);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/Account/register/", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
