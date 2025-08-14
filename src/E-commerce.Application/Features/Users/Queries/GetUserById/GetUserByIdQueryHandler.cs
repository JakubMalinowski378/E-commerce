using AutoMapper;
using E_commerce.Application.Features.Users.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Users.Queries.GetUserById;
public class GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserId);

        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId.ToString());

        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }
}
