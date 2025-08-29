using AutoMapper;
using E_commerce.Application.Features.Users.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(
    IUserRepository userRepository,
    IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId)
            ?? throw new NotFoundException(nameof(User), request.UserId.ToString());

        var userDto = mapper.Map<UserDto>(user);
        return userDto;
    }
}
