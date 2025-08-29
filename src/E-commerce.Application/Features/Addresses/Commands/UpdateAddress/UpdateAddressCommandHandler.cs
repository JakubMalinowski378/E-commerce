using AutoMapper;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandHandler(
    IRepository<Address> addressRepository,
    IAuthorizationService authorizationService,
    IMapper mapper,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAddressCommand>
{
    public async Task Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await addressRepository.GetByIdAsync(request.AddressId)
            ?? throw new NotFoundException(nameof(Address), request.AddressId.ToString());

        if (!await authorizationService.HasPermission(address, ResourceOperation.Update))
            throw new ForbidException();

        mapper.Map(request, address);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
