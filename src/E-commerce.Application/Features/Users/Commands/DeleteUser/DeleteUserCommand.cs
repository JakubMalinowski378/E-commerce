using MediatR;

namespace E_commerce.Application.Features.Users.Commands.DeleteUser;
public class DeleteUserCommand(Guid userId) : IRequest
{
    public Guid UserId { get; set; } = userId;
}
