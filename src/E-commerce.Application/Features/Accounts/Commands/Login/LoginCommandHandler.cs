using E_commerce.Application.Features.Accounts.Dtos;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Application.Features.Accounts.Commands.Login;

public class LoginCommandHandler(
    IUserRepository userRepository,
    ITokenService tokenService,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
    : IRequestHandler<LoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, u => u.Include(x => x.Role))
            ?? throw new UnauthorizedException("Invalid username or password");

        if (!passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid username or password");

        var (accessToken, refreshToken) = tokenService.GenerateTokens(user);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = DateTime.UtcNow.AddDays(30);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponse(accessToken, refreshToken);
    }
}