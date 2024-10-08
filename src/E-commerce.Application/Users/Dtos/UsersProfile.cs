﻿using AutoMapper;
using E_commerce.Application.Users.Commands.RegisterUser;
using E_commerce.Domain.Entities;

namespace E_commerce.Application.Users.Dtos;
public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<RegisterUserCommand, User>();
        CreateMap<User, UserDto>();
    }
}
