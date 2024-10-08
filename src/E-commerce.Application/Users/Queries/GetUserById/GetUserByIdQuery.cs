﻿using E_commerce.Application.Users.Dtos;
using MediatR;

namespace E_commerce.Application.Users.Queries.GetUserById;
public class GetUserByIdQuery(Guid userId) : IRequest<UserDto>
{
    public Guid UserId { get; set; } = userId;
}
