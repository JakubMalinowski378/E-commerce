using MediatR;

namespace E_commerce.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand(Guid userId) : IRequest<Guid>
    {
        public Guid UserId { get; set; } = userId;
    }
}
