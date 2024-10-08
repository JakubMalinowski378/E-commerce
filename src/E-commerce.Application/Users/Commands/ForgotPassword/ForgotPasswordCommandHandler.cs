﻿using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Users.Commands.ForgotPassword;
public class ForgotPasswordCommandHandler(IEmailNotificationService emailNotificationService,
    IUserRepository userRepository) : IRequestHandler<ForgotPasswordCommand>
{
    private readonly IEmailNotificationService _emailNotificationService = emailNotificationService;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email)
            ?? throw new NotFoundException(nameof(User), request.Email);
        user.ResetPasswordToken = Guid.NewGuid().ToString();
        user.ResetPasswordTokenExpiration = DateTime.UtcNow.AddHours(3);

        await _emailNotificationService.SendResetPasswordEmailAsync(user.Email, user.ResetPasswordToken);
        await _userRepository.SaveChanges();
    }
}
