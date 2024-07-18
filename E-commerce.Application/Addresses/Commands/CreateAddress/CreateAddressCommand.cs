using MediatR;
using System.Text.Json.Serialization;

namespace E_commerce.Application.Addresses.Commands.CreateAddress;
public class CreateAddressCommand : IRequest<Guid>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public int StreetNumber { get; set; }
    public int? ApartmentNumber { get; set; }
    public string Street { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
