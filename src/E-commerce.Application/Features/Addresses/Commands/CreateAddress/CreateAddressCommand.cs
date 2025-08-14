using MediatR;

namespace E_commerce.Application.Features.Addresses.Commands.CreateAddress;
public class CreateAddressCommand : IRequest<Guid>
{
    public int StreetNumber { get; set; }
    public int? ApartmentNumber { get; set; }
    public string Street { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
