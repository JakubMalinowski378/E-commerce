using AutoMapper;
using E_commerce.Application.Features.Addresses.Commands.CreateAddress;
using E_commerce.Application.Features.Addresses.Commands.UpdateAddress;
using E_commerce.Domain.Entities;

namespace E_commerce.Application.Features.Addresses.Commands.Dtos;
public class AddressesProfile : Profile
{
    public AddressesProfile()
    {
        CreateMap<CreateAddressCommand, Address>();
        CreateMap<Address, AddressDto>();
        CreateMap<UpdateAddressCommand, Address>();
    }
}
