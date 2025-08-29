using E_commerce.Application.Features.Accounts.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQuery(Guid userId) : IRequest<UserDto>
{
    public Guid UserId { get; set; } = userId;
}
