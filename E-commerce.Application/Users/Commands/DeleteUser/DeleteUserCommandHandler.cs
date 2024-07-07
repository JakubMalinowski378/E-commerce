using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler :IRequestHandler<DeleteUserCommand,Guid>
    {

        private IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Guid>Handle(DeleteUserCommand request,CancellationToken cancel)
        {
            await _userRepository.DeleteUserAsync(request.UserId);
            return request.UserId;
        }

        
    }
}
