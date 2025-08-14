using AutoMapper;
using E_commerce.Application.Features.Addresses.Commands.Dtos;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Addresses.Queries.GetAddressById;
public class GetAddressByIdQueryHandler(IMapper mapper,
    IAddressRepository addressRepository)
    : IRequestHandler<GetAddressByIdQuery, AddressDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IAddressRepository _addressRepository = addressRepository;

    public async Task<AddressDto> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.AddressId);
        if (address == null)
            throw new NotFoundException(nameof(Address), request.AddressId.ToString());
        var addressDto = _mapper.Map<AddressDto>(address);
        return addressDto;
    }
}
