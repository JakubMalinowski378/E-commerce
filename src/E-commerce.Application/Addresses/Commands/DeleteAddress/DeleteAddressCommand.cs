using MediatR;

namespace E_commerce.Application.Addresses.Commands.DeleteAddress;
public class DeleteAddressCommand(Guid addressId) : IRequest
{
    public Guid AddressId { get; set; } = addressId;
}
