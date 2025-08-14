using E_commerce.Application.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher)
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
            await unitOfWork.SaveChangesAsync(cancellationToken);
            throw new InvalidOperationException("Token has expired");
        }

        user.PasswordHash = passwordHasher.Hash(request.NewPassword);
        user.ResetPasswordToken = null;
        user.ResetPasswordTokenExpiration = null;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
