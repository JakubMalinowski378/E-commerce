using E_commerce.Application.Users.Dtos;
using MediatR;

namespace E_commerce.Application.Users.Commands.RegisterUser;
public class RegisterUserCommand : RegisterDto, IRequest<Guid>
{
}
