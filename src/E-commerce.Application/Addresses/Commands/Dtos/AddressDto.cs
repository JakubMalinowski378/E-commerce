namespace E_commerce.Application.Addresses.Commands.Dtos;
public class AddressDto
{
    public int StreetNumber { get; set; }
    public int? ApartmentNumber { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
