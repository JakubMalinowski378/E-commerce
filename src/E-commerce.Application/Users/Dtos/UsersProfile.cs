using AutoMapper;
using E_commerce.Domain.Entities;

namespace E_commerce.Application.Users.Dtos;
public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, UserDto>();
    }
}
