using E_commerce.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;

namespace E_commerce.Application.Features.Accounts.Commands.PasswordResetRequest;

public class PasswordResetRequestCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ResetPasswordRequestCommand>
{
    public async Task Handle(ResetPasswordRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken: cancellationToken);

        if (user is null)
            return;

        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        user.ResetPasswordToken = token;
        user.ResetPasswordTokenExpiration = DateTime.UtcNow.AddHours(1);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Send email with token
    }
}
