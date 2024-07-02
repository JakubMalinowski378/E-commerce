using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Application.Users.Commands.RegisterUser;
internal class RegisterUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.UserExists(request.Email))
            throw new NotFoundException(nameof(User), request.Email);

        using var hmac = new HMACSHA512();
        var user = new User
        {
            Firstname = request.Firstname,
            LastName = request.LastName,
            Email = request.Email,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber,
            PaswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Pasword)),
            PasswordSalt = hmac.Key
        };
        await _userRepository.Create(user);
        return user.Id;
    }
}
