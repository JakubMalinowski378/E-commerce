using AutoMapper;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Application.Users.Commands.RegisterUser;
internal class RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.UserExists(request.Email))
            throw new NotFoundException(nameof(User), request.Email);

        using var hmac = new HMACSHA512();

        var user = _mapper.Map<User>(request);

        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        user.PasswordSalt = hmac.Key;

        await _userRepository.Create(user);
        return user.Id;
    }
}
