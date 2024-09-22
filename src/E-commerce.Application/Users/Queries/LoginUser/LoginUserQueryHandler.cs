﻿using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Application.Users.Dtos;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Application.Users.Queries.LoginUser;

public class LoginUserQueryHandler(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
    : IRequestHandler<LoginUserQuery, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email, u => u.Roles);
        if (user == null)
            throw new UnauthorizedException("Invalid username or password");

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

        if (!computedHash.SequenceEqual(user.PasswordHash))
            throw new UnauthorizedException("Invalid username or password");

        var userDto = _mapper.Map<UserDto>(user);
        userDto.Token = _tokenService.CreateToken(user);

        return userDto;
    }
}