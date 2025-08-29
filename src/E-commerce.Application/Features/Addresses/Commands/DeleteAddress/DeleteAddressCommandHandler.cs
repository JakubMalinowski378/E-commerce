using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommandHandler(
    IRepository<CartItem> addressRepository,
    IAuthorizationService authorizationService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAddressCommand>
{
    public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await addressRepository.GetByIdAsync(request.AddressId)
            ?? throw new NotFoundException(nameof(Address), request.AddressId.ToString());

        if (!await authorizationService.HasPermission(address, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }

        addressRepository.Remove(address);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
