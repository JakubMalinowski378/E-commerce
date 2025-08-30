using AutoMapper;
using E_commerce.Application.Features.Accounts.Dtos;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IMapper mapper,
    ITokenService tokenService,
    IPasswordHasher passwordHasher,
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterUserCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.IsEmailInUseAsync(request.Email))
            throw new ConflictException($"Email {request.Email} is in use");

        if (await userRepository.IsPhoneNumberInUseAsync(request.PhoneNumber))
            throw new ConflictException($"Phone number {request.PhoneNumber} is in use");

        var user = mapper.Map<User>(request);

        var userRole = await roleRepository.GetByNameAsync("User")
            ?? throw new InvalidOperationException("Role 'User' not found. Please contact with support");

        user.PasswordHash = passwordHasher.Hash(request.Password);
        user.EmailConfirmed = false;
        user.ConfirmationToken = Guid.NewGuid().ToString();
        user.ConfirmationTokenExpiration = DateTime.UtcNow.AddDays(1);
        user.Role = userRole;

        var (accessToken, refreshToken) = tokenService.GenerateTokens(user);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = DateTime.UtcNow.AddDays(30);

        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponse(accessToken, refreshToken);
    }
}
