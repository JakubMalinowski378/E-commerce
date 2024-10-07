using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Application.Users.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Application.Users.Commands.RegisterUser;
public class RegisterUserCommandHandler(IEmailNotificationService emailNotificationService,
    IUserRepository userRepository,
    IMapper mapper,
    ITokenService tokenService
    )
    : IRequestHandler<RegisterUserCommand, JwtToken>
{
    private readonly IEmailNotificationService _emailNotificationService = emailNotificationService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<JwtToken> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.UserExists(request.Email))
            throw new ConflictException($"Email {request.Email} is in use");

        using var hmac = new HMACSHA512();

        var user = _mapper.Map<User>(request);

        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        user.PasswordSalt = hmac.Key;
        user.EmailConfirmed = false;
        user.ConfirmationToken = Guid.NewGuid().ToString();
        user.ConfirmationTokenExpiration = DateTime.UtcNow.AddDays(1);

        await _userRepository.Create(user);
        await _emailNotificationService.SendConfirmationEmailAsync(user.Email, user.ConfirmationToken);

        var jwtToken = new JwtToken(_tokenService.CreateToken(user));

        return jwtToken;
    }
}
