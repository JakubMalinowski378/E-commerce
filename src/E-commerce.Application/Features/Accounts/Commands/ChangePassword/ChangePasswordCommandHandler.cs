using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommandHandler(
    IUserRepository userRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher)
    : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await userRepository.GetByEmailAsync(currentUser!.Email, cancellationToken: cancellationToken)
            ?? throw new NotFoundException(nameof(User), currentUser.Email);

        if (!passwordHasher.Verify(request.OldPassword, user.PasswordHash))
            throw new ForbidException("Old password is incorrect");

        user.PasswordHash = passwordHasher.Hash(request.NewPassword);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}