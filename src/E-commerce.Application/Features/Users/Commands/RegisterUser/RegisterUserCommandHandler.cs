using AutoMapper;
using E_commerce.Application.Features.Users.Dtos;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;

namespace E_commerce.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(IEmailNotificationService emailNotificationService,
    IUserRepository userRepository,
    IMapper mapper,
    ITokenService tokenService,
    IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterUserCommand, JwtToken>
{
    public async Task<JwtToken> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.UserExists(request.Email))
            throw new ConflictException($"Email {request.Email} is in use");

        using var hmac = new HMACSHA512();

        var user = mapper.Map<User>(request);

        user.PasswordHash = passwordHasher.Hash(request.Password);
        user.EmailConfirmed = false;
        user.ConfirmationToken = Guid.NewGuid().ToString();
        user.ConfirmationTokenExpiration = DateTime.UtcNow.AddDays(1);

        await userRepository.AddAsync(user);
        await emailNotificationService.SendConfirmationEmailAsync(user.Email, user.ConfirmationToken);

        var jwtToken = new JwtToken(tokenService.CreateToken(user));

        return jwtToken;
    }
}
