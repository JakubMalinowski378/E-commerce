using AutoMapper;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancel)
    {
        if (!await _userRepository.UserExists(request.Email))
            throw new NotFoundException(nameof(User), request.Email);
        var user = _mapper.Map<User>(request);
        return user.Id;
    }
}
