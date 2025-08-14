using MediatR;

namespace E_commerce.Application.Features.Addresses.Commands.DeleteAddress;
public class DeleteAddressCommand(Guid addressId) : IRequest
{
    public Guid AddressId { get; set; } = addressId;
}
