using AutoMapper;
using E_commerce.Application.Features.Addresses.Commands.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Addresses.Queries.GetAddressById;

public class GetAddressByIdQueryHandler(
    IRepository<Address> addressRepository,
    IMapper mapper)
    : IRequestHandler<GetAddressByIdQuery, AddressDto>
{
    public async Task<AddressDto> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await addressRepository.GetByIdAsync(request.AddressId);
        if (address is null)
            throw new NotFoundException(nameof(Address), request.AddressId.ToString());
        var addressDto = mapper.Map<AddressDto>(address);
        return addressDto;
    }
}
