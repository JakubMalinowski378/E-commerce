using E_commerce.Application.Users.Dtos;
using MediatR;

namespace E_commerce.Application.Users.Queries.GetUsers;
public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
{
}
