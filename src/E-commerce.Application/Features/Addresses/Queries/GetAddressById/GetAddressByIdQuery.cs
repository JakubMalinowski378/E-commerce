using E_commerce.Application.Features.Addresses.Commands.Dtos;
using MediatR;

namespace E_commerce.Application.Features.Addresses.Queries.GetAddressById;

public class GetAddressByIdQuery(Guid addressId) : IRequest<AddressDto>
{
    public Guid AddressId { get; set; } = addressId;
}
