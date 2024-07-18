using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Application.Users.Commands.RegisterUser;
public class RegisterUserCommandHandler(IEmailSender emailSender, IUserRepository userRepository, IRolesRepository rolesRepository, IMapper mapper)
    : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IRolesRepository _rolesRepository = rolesRepository;
    public readonly IEmailSender _emailSender = emailSender;
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.UserExists(request.Email))
            throw new NotFoundException(nameof(User), request.Email);

        using var hmac = new HMACSHA512();

        var user = _mapper.Map<User>(request);

        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        user.PasswordSalt = hmac.Key;
        user.EmailConfirmed = false;
        user.ConfirmationToken = Guid.NewGuid().ToString();
        user.ConfirmationTokenExpiration = DateTime.UtcNow.AddDays(1);
        var tempraryAppUrl = "https://localhost:7202";
        var confirmationUrl = $"{tempraryAppUrl}/api/Mail/confirm-email?token={user.ConfirmationToken}&email={user.Email}";
        var message = $"Please confirm your email by clicking on the following link: <a href='{confirmationUrl}'>Confirm Email .</a>";

        await _emailSender.SendEmailAsync(user.Email, "Confirm your email", message);
        await _userRepository.Create(user);
        return user.Id;
    }
}
