using E_commerce.Application.Features.Users.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Users.Queries.GetUsers;
public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
{
}
