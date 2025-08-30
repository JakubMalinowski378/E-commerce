using E_commerce.Application.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.PasswordReset;

public class PasswordResetCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher)
    : IRequestHandler<PasswordResetCommand>
{
    public async Task Handle(PasswordResetCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FirstOrDefaultAsync(u => u.Email == request.Email,
            cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Invalid token");

        if (user.ResetPasswordTokenExpiration < DateTime.UtcNow)
        {
            user.ResetPasswordToken = null;
            user.ResetPasswordTokenExpiration = null;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            throw new InvalidOperationException("Token has expired");
        }

        if (user.ResetPasswordToken != request.Token)
        {
            throw new InvalidOperationException("Invalid token");
        }

        user.PasswordHash = passwordHasher.Hash(request.NewPassword);
        user.ResetPasswordToken = null;
        user.ResetPasswordTokenExpiration = null;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
