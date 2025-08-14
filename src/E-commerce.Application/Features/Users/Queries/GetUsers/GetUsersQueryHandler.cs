using AutoMapper;
using E_commerce.Application.Features.Users.Dtos;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Users.Queries.GetUsers;
public class GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsersAsync();

        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

        return userDtos;
    }
}
