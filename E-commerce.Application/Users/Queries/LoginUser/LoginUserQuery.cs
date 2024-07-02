using E_commerce.Application.Users.Dtos;
using MediatR;

namespace E_commerce.Application.Users.Queries.LoginUser;
public class LoginUserQuery : LoginDto, IRequest<UserDto>
{
}