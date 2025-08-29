using AutoMapper;
using E_commerce.Application.Features.Addresses.Commands.Dtos;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Addresses.Queries.GetUserAddresses;

public class GetUserAddressesQueryHandler(
    IRepository<Address> addressRepository,
    IAuthorizationService authorizationService,
    IMapper mapper)
    : IRequestHandler<GetUserAddressesQuery, IEnumerable<AddressDto>>
{
    public async Task<IEnumerable<AddressDto>> Handle(GetUserAddressesQuery request, CancellationToken cancellationToken)
    {
        var addresses = await addressRepository.FindAsync(a => a.UserId == request.UserId);

        foreach (var address in addresses)
        {
            if (!await authorizationService.HasPermission(address, ResourceOperation.Read))
            {
                throw new ForbidException();
            }
        }

        var addressDtos = mapper.Map<IEnumerable<AddressDto>>(addresses);

        return addressDtos;
    }
}
