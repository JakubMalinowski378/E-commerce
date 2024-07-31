using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.ChangeEmail
{
    public class ChangeEmailCommandHandler(IUserRepository userRepository,IEmailSender emailSender,IUserContext userContext)
        : IRequestHandler<ChangeEmailCommand>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IEmailSender _emailSender = emailSender;
       

        public async Task Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId);
            if (user == null) 
            throw new NotFoundException(nameof(User), request.UserId.ToString());
            user.Email = request.Email;
            user.EmailConfirmed = false;
            user.ConfirmationTokenExpiration = DateTime.UtcNow.AddDays(1);
            user.ConfirmationToken = Guid.NewGuid().ToString();
            var tempraryAppUrl = "https://localhost:7202";
            var confirmationUrl = $"{tempraryAppUrl}/api/Mail/confirm-email?token={user.ConfirmationToken}&email={user.Email}";
            var message = $"Please confirm your email by clicking on the following link: <a href='{confirmationUrl}'>Confirm Email .</a>";
            await _emailSender.SendEmailAsync(user.Email, "Confirm your email", message);
            await _userRepository.SaveChanges();

        }
    }
}
