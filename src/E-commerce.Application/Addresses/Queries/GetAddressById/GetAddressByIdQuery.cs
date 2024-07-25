using E_commerce.Application.Addresses.Commands.Dtos;
using MediatR;

namespace E_commerce.Application.Addresses.Queries.GetAddressById;
public class GetAddressByIdQuery(Guid addressId) : IRequest<AddressDto>
{
    public Guid AddressId { get; set; } = addressId;
}
