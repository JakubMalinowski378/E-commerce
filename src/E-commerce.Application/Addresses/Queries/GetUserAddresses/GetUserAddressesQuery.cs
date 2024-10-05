using E_commerce.Application.Addresses.Commands.Dtos;
using MediatR;

namespace E_commerce.Application.Addresses.Queries.GetUserAddresses;

public class GetUserAddressesQuery(Guid userId) : IRequest<IEnumerable<AddressDto>>
{
    public Guid UserId { get; set; } = userId;
}
