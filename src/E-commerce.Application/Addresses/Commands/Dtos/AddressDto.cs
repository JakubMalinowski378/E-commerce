namespace E_commerce.Application.Addresses.Commands.Dtos;
public class AddressDto
{
    public string StreetNumber { get; set; } = string.Empty;
    public string? ApartmentNumber { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
