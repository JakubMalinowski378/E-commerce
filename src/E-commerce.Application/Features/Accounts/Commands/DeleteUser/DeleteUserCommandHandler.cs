using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Application.Features.Accounts.Commands.DeleteUser;

public class DeleteUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId,
            u => u.Include(x => x.Ratings)
                  .Include(x => x.Ratings),
            cancellationToken: cancellationToken)
            ?? throw new NotFoundException(nameof(User), request.UserId.ToString());

        userRepository.Remove(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
