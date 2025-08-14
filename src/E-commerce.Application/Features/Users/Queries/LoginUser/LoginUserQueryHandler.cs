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
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<JwtToken> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email, u => u.Role)
            ?? throw new UnauthorizedException("Invalid username or password");

        var computedHash = passwordHasher.Hash(request.Password);

        if (!passwordHasher.Verify(user.PasswordHash, computedHash))
            throw new UnauthorizedException("Invalid username or password");

        var jwtToken = new JwtToken(_tokenService.CreateToken(user));
        return jwtToken;
    }
}