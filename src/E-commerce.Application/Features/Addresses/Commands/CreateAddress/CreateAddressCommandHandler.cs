using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IAddressRepository addressRepository,
    IAuthorizationService authorizationService)
    : IRequestHandler<CreateAddressCommand, Guid>
{
    public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = mapper.Map<Address>(request);

        if (!await authorizationService.HasPermission(address, ResourceOperation.Create))
        {
            throw new ForbidException();
        }

        var user = userContext.GetCurrentUser()
            ?? throw new ForbidException();
        address.UserId = user.Id;

        await addressRepository.Create(address);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return address.Id;
    }
}
