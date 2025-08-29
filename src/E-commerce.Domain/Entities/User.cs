namespace E_commerce.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public char Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string PasswordHash { get; set; } = default!;
    public bool EmailConfirmed { get; set; }
    public string? ConfirmationToken { get; set; }
    public DateTime? ConfirmationTokenExpiration { get; set; }
    public string? ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordTokenExpiration { get; set; }
    public List<Address> Addresses { get; set; } = default!;
    public List<CartItem> CartItems { get; set; } = default!;
    public List<Rating> Ratings { get; set; } = default!;
    public Role Role { get; set; } = default!;
    public List<Product>? Products { get; set; }
}