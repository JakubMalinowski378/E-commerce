using AutoMapper;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Addresses.Commands.CreateAddress;
public class CreateAddressCommandHandler(IAddressRepository addressRepository,
    IMapper mapper,
    IAddressAuthorizationService addressAuthorizationService)
    : IRequestHandler<CreateAddressCommand, Guid>
{
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IAddressAuthorizationService _addressAuthorizationService = addressAuthorizationService;

    public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = _mapper.Map<Address>(request);
        if (!_addressAuthorizationService.Authorize(address, ResourceOperation.Create))
        {
            throw new ForbidException();
        }
        await _addressRepository.Create(address);
        return address.Id;
    }
}
