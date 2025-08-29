using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.DeleteUser;
public class DeleteUserCommand(Guid userId) : IRequest
{
    public Guid UserId { get; set; } = userId;
}
