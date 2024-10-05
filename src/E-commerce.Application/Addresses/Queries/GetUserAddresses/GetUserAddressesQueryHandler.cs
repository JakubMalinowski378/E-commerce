using AutoMapper;
using E_commerce.Application.Addresses.Commands.Dtos;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Addresses.Queries.GetUserAddresses;
public class GetUserAddressesQueryHandler(IAddressRepository addressRepository,
    IAddressAuthorizationService _addressAuthorizationService,
    IMapper mapper)
    : IRequestHandler<GetUserAddressesQuery, IEnumerable<AddressDto>>
{
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<AddressDto>> Handle(GetUserAddressesQuery request, CancellationToken cancellationToken)
    {
        var addresses = await _addressRepository.GetUserAddressesAsync(request.UserId);

        foreach (var address in addresses)
        {
            if (!_addressAuthorizationService.Authorize(address, ResourceOperation.Read))
            {
                throw new ForbidException();
            }
        }

        var addressDtos = _mapper.Map<IEnumerable<AddressDto>>(addresses);

        return addressDtos;
    }
}
