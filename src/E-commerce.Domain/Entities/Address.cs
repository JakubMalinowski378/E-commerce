namespace E_commerce.Domain.Entities;
public class Address
{
    public Guid Id { get; set; }
    public int StreetNumber { get; set; }
    public int? ApartmentNumber { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public User User { get; set; } = default!;
    public Guid UserId { get; set; }
}
