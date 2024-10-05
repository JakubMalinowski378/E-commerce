using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Addresses.Commands.CreateAddress;
public class CreateAddressCommandHandler(IAddressRepository addressRepository,
    IMapper mapper,
    IAddressAuthorizationService addressAuthorizationService,
    IUserContext userContext)
    : IRequestHandler<CreateAddressCommand, Guid>
{
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IAddressAuthorizationService _addressAuthorizationService = addressAuthorizationService;
    private readonly IUserContext _userContext = userContext;

    public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = _mapper.Map<Address>(request);
        if (!_addressAuthorizationService.Authorize(address, ResourceOperation.Create))
        {
            throw new ForbidException();
        }
        var user = _userContext.GetCurrentUser()
            ?? throw new ForbidException();
        address.UserId = user.Id;
        await _addressRepository.Create(address);
        return address.Id;
    }
}
