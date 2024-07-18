using E_commerce.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Application.Email.Commands.ResetPassword;
public class ResetPasswordCommandHandler(IUserRepository userRepository) : IRequestHandler<ResetPasswordCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByResetPasswordTokenAsync(request.Token);
        if (user == null || user.ResetPasswordTokenExpiration < DateTime.UtcNow) throw new Exception("Invalid confirmation Token");
        var hmac = new HMACSHA512();
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        user.PasswordSalt = hmac.Key;
        await _userRepository.SaveUserAsync();

    }
}
