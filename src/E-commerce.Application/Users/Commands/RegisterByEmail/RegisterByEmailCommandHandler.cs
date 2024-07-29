using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.RegisterByEmail
{



    public class RegisterByEmailCommandHandler(IEmailSender emailSender, IUserRepository userRepository, IRolesRepository rolesRepository) : IRequestHandler<RegisterByEmailCommand, Guid>
    {
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRolesRepository _rolesRepository = rolesRepository;

        public async Task<Guid> Handle(RegisterByEmailCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.UserExists(request.Email))
                throw new NotFoundException(nameof(User), request.Email);
            var user = new User { Email = request.Email ,DateOfBirth= DateOnly.FromDateTime(request.BirthDate) };
            var hmac = new HMACSHA512();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            user.Login = "Client:"+Guid.NewGuid().ToString();
            user.PasswordSalt = hmac.Key;
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
}
