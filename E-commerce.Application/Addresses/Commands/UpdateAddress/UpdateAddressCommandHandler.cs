using AutoMapper;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Addresses.Commands.UpdateAddress;
public class UpdateAddressCommandHandler(IAddressRepository addressRepository,
    IAddressAuthorizationService addressAuthorizationService,
    IMapper mapper)
    : IRequestHandler<UpdateAddressCommand>
{
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly IAddressAuthorizationService _addressAuthorizationService = addressAuthorizationService;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.AddressId)
            ?? throw new NotFoundException(nameof(Address), request.AddressId.ToString());

        if (!_addressAuthorizationService.Authorize(address, ResourceOperation.Update))
            throw new ForbidException();

        _mapper.Map(request, address);

        await _addressRepository.SaveChanges();
    }
}
