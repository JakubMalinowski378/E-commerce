﻿using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Users.Commands.ConfirmEmail;
public class ConfirmEmailCommandHandler(IUserRepository userRepository) : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByConfirmationTokenAsync(request.Token)
            ?? throw new NotFoundException(nameof(User), request.Token);

        if (user.ConfirmationTokenExpiration < DateTime.UtcNow)
        {
            user.ConfirmationToken = null;
            user.ConfirmationTokenExpiration = null;
            throw new InvalidOperationException("Invalid confirmation Token");
        }
        user.EmailConfirmed = true;
        user.ConfirmationTokenExpiration = null;
        user.ConfirmationToken = null;
        await _userRepository.SaveChanges();
    }
}
