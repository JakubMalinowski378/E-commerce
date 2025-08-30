using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ConfirmEmailCommand>
{
    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository
            .FirstOrDefaultAsync(u => u.ConfirmationToken == request.Token && u.Id == request.UserId,
                                 cancellationToken: cancellationToken)
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
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
