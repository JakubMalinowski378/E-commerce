using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Users.Commands.UpdatePassword;

public class UpdatePasswordCommandHandler(
    IUserRepository userRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher)
    : IRequestHandler<UpdatePasswordCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserContext _userContext = userContext;

    public async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var userContext = _userContext.GetCurrentUser();

        var user = await _userRepository.GetUserByEmailAsync(userContext!.Email)
            ?? throw new NotFoundException(nameof(User), userContext.Email);

        var computedHash = passwordHasher.Hash(request.OldPassword);

        if (!passwordHasher.Verify(computedHash, user.PasswordHash))
            throw new ForbidException("Old password is incorrect");

        user.PasswordHash = passwordHasher.Hash(request.NewPassword);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}