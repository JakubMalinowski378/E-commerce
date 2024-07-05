namespace E_commerce.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public char Gender { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;
    public bool EmailConfirmed { get; set; }
    public virtual List<Address> Addresses { get; set; } = default!;
    public virtual Cart Cart { get; set; } = default!;
    public virtual List<Rating> Ratings { get; set; } = default!;
}