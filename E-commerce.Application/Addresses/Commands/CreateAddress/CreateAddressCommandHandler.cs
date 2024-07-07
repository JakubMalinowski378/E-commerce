using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Addresses.Commands.CreateAddress;
public class CreateAddressCommandHandler(IAddressRepository addressRepository,
    IMapper mapper,
    IUserContext userContext)
    : IRequestHandler<CreateAddressCommand, Guid>
{
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUserContext _userContext = userContext;

    public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();

        var address = _mapper.Map<Address>(request);

        address.UserId = user!.Id;

        await _addressRepository.Create(address);
        return address.Id;
    }
}
