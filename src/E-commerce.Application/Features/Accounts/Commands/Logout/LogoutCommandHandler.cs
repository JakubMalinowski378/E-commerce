using E_commerce.Application.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.Logout;

public class LogoutCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IUserContext userContext)
    : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var userCtx = userContext.GetCurrentUser()
            ?? throw new UnauthorizedAccessException("User is not authenticated");

        var user = await userRepository.GetByIdAsync(userCtx.Id)
            ?? throw new KeyNotFoundException("User not found");

        user.RefreshToken = null;
        user.RefreshTokenExpires = null;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
