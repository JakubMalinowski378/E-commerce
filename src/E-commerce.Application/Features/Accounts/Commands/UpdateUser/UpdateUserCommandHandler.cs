using AutoMapper;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Accounts.Commands.UpdateUser;

public class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IMapper mapper)
    : IRequestHandler<UpdateUserCommand, Guid>
{
    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancel)
    {
        if (!await userRepository.IsEmailInUseAsync(request.Email))
            throw new NotFoundException(nameof(User), request.Email);
        var user = mapper.Map<User>(request);
        return user.Id;
    }
}
