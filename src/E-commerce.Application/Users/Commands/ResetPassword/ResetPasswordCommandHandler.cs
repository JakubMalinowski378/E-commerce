using E_commerce.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Application.Users.Commands.ResetPassword;
public class ResetPasswordCommandHandler(IUserRepository userRepository)
    : IRequestHandler<ResetPasswordCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByResetPasswordTokenAsync(request.Token)
            ?? throw new InvalidOperationException("Invalid reset token");

        if (user.ResetPasswordTokenExpiration < DateTime.UtcNow)
        {
            user.ResetPasswordToken = null;
            user.ResetPasswordTokenExpiration = null;
            await _userRepository.SaveChanges();
            throw new InvalidOperationException("Token has expired");
        }

        var hmac = new HMACSHA512();
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword));
        user.PasswordSalt = hmac.Key;
        user.ResetPasswordToken = null;
        user.ResetPasswordTokenExpiration = null;
        await _userRepository.SaveChanges();
    }
}
