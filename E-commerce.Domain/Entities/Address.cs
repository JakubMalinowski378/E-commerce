namespace E_commerce.Domain.Entities;
public class Address
{
    public int StreetNumber { get; set; }
    public int? ApartmentNumber { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
