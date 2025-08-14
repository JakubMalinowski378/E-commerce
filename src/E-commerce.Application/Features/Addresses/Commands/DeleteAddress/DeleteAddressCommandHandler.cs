using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Addresses.Commands.DeleteAddress;
public class DeleteAddressCommandHandler(IAddressRepository addressRepository,
    IAddressAuthorizationService addressAuthorizationService)
    : IRequestHandler<DeleteAddressCommand>
{
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly IAddressAuthorizationService _addressAuthorizationService = addressAuthorizationService;

    public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.AddressId)
            ?? throw new NotFoundException(nameof(Address), request.AddressId.ToString());

        if (!_addressAuthorizationService.Authorize(address, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }

        await _addressRepository.Delete(address);
    }
}
