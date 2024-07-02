using E_commerce.Application.Interfaces;
using E_commerce.Application.Users.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Application.Users.Queries.LoginUser;

public class LoginUserQueryHandler(IUserRepository userRepository, ITokenService tokenService)
    : IRequestHandler<LoginUserQuery, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<UserDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user == null)
            throw new NotFoundException(nameof(User), request.Email);

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PaswordHash[i])
                throw new NotFoundException(nameof(User), request.Email);
        }
        return new UserDto
        {
            Email = user.Email,
            Firstname = user.Firstname,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Addresses = user.Addresses,
            EmailConfirmed = user.EmailConfirmed,
            Gender = user.Gender,
            Token = _tokenService.CreateToken(user)
        };
    }
}