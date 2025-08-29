using E_commerce.Application.Features.Accounts.Dtos;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenService tokenService)
    : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByRefreshTokenAsync(request.RefreshToken)
            ?? throw new InvalidOperationException("Invalid refresh token.");

        if (user.RefreshTokenExpires <= DateTime.UtcNow)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpires = null;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            throw new InvalidOperationException("Refresh token has expired.");
        }

        var (accessToken, refreshToken) = tokenService.GenerateTokens(user);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = DateTime.UtcNow.AddDays(30);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponse(accessToken, refreshToken);
    }
}
