using E_commerce.API.Middlewares;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace E_commerce.API.Tests.Middlewares;
public class ErrorHandlingMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
    {
        var middleware = new ErrorHandlingMiddleware();
        var context = new DefaultHttpContext();
        var nextDelegate = new Mock<RequestDelegate>();

        await middleware.InvokeAsync(context, nextDelegate.Object);

        nextDelegate.Verify(next => next.Invoke(context), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode403()
    {
        var middleware = new ErrorHandlingMiddleware();
        var context = new DefaultHttpContext();
        var exception = new ForbidException();

        await middleware.InvokeAsync(context, _ => throw exception);
        context.Response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }

    [Fact]
    public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode404()
    {
        var middleware = new ErrorHandlingMiddleware();
        var context = new DefaultHttpContext();
        var exception = new NotFoundException(nameof(Product),
            "04EAB672-D55E-41BD-B7FE-AB2E98939835");

        await middleware.InvokeAsync(context, _ => throw exception);

        context.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task InvokeAsync_WhenUnauthorizedExceptionThrown_ShouldSetStatusCode401()
    {
        var middleware = new ErrorHandlingMiddleware();
        var context = new DefaultHttpContext();
        var exception = new UnauthorizedException("Invalid username or password");

        await middleware.InvokeAsync(context, _ => throw exception);

        context.Response.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }

    [Fact]
    public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldSetStatusCode500()
    {
        var middleware = new ErrorHandlingMiddleware();
        var context = new DefaultHttpContext();
        var exception = new Exception();

        await middleware.InvokeAsync(context, _ => throw exception);

        context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
}
