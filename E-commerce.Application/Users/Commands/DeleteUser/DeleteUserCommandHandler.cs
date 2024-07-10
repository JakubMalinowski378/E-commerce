using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Users.Commands.DeleteUser;
public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserId,
            u => u.CartItems,
            u => u.Ratings,
            u => u.Products);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId.ToString());

        await _userRepository.DeleteUser(user);
    }
}
