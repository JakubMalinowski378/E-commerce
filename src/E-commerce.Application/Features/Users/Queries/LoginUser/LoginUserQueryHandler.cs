using E_commerce.Application.Features.Users.Dtos;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Users.Queries.LoginUser;

public class LoginUserQueryHandler(
    IUserRepository userRepository,
    ITokenService tokenService,
    IPasswordHasher passwordHasher)
    : IRequestHandler<LoginUserQuery, JwtToken>
{
    public async Task<JwtToken> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Email, u => u.Role)
            ?? throw new UnauthorizedException("Invalid username or password");

        if (!passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid username or password");

        var jwtToken = new JwtToken(tokenService.CreateToken(user));
        return jwtToken;
    }
}