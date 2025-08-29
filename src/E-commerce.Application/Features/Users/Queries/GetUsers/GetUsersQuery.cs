using E_commerce.Application.Features.Accounts.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Users.Queries.GetUsers;
public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
{
}
