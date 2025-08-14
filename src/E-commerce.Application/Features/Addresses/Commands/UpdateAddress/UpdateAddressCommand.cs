using MediatR;
using System.Text.Json.Serialization;

namespace E_commerce.Application.Features.Addresses.Commands.UpdateAddress;
public class UpdateAddressCommand : IRequest
{
    [JsonIgnore]
    public Guid AddressId { get; set; }
    public int StreetNumber { get; set; }
    public int? ApartmentNumber { get; set; }
    public string Street { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
