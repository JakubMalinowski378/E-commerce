using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;


namespace E_commerce.Application.Email.Commands.ForgotPassword;
public class ForgotPasswordCommandHandler(IEmailSender emailSender, IUserRepository userRepository) : IRequestHandler<ForgotPasswordCommand>
{
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email)
            ?? throw new NotFoundException(nameof(User), request.Email);
        user.ResetPasswordToken = Guid.NewGuid().ToString();
        user.ResetPasswordTokenExpiration = DateTime.UtcNow.AddHours(3);
        var message = $"Your reset password code is {user.ResetPasswordToken}";

        await _emailSender.SendEmailAsync(user.Email, "Reset Password", message);
        await _userRepository.SaveChanges();
    }
}
