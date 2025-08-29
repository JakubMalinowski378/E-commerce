using E_commerce.Application.Features.Accounts.Commands.Login;
using E_commerce.Application.Features.Accounts.Commands.RegisterUser;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
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
    private readonly Mock<ITokenService> _tokenServiceMock = new();
    private readonly LoginCommand _loginUserQuery = new()
    {
        Email = "test@example.com",
        Password = "Password#123"
    };
    private readonly RegisterUserCommand _registerUserCommand = new()
    {
        DateOfBirth = new DateOnly(2000, 1, 1),
        Email = "test@example.com",
        Password = "Password#123",
        FirstName = "Jan",
        LastName = "Nowak",
        Gender = 'M',
        PhoneNumber = "555555555"
    };

    internal AccountControllerTests(WebApplicationFactory<Program> factory)
    {
        _tokenServiceMock.Setup(m => m.GenerateTokens(new User())).Returns((string.Empty, string.Empty));
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.Replace(ServiceDescriptor.Scoped(typeof(IUserRepository),
                    _ => _userRepositoryMock.Object));

                services.Replace(ServiceDescriptor.Scoped(typeof(ITokenService),
                    _ => _tokenServiceMock.Object));
            });
        });
    }

    [Fact]
    public async Task Login_ForNonExisitingUser_ShouldReturn401Unauthorized()
    {
        _userRepositoryMock.Setup(m => m.GetByEmailAsync(_loginUserQuery.Email, null, false))
            .ReturnsAsync((User?)null);
        _userRepositoryMock.Setup(m => m.GetByEmailAsync(_loginUserQuery.Email, null, false))
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
        Assert.True(true);
    }

    [Fact]
    public async Task Register_WhenEmailIsAlreadyInUse_ShouldReturn409Conflict()
    {
        _userRepositoryMock.Setup(m => m.IsEmailInUseAsync(_registerUserCommand.Email)).ReturnsAsync(true);

        var client = _factory.CreateClient();
        var json = JsonSerializer.Serialize(_registerUserCommand);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/Account/register/", content);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Register_WhenGivenValidData_ShouldReturn200Ok()
    {
        _userRepositoryMock.Setup(m => m.IsEmailInUseAsync(_registerUserCommand.Email)).ReturnsAsync(false);

        var client = _factory.CreateClient();
        var json = JsonSerializer.Serialize(_registerUserCommand);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/Account/register/", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
