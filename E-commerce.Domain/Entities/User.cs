﻿namespace E_commerce.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public char Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;
    public bool EmailConfirmed { get; set; }
    public string? ConfirmationToken { get; set; }
    public DateTime? ConfirmationTokenExpiration { get; set; }
    public string? ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordTokenExpiration { get; set; }
    public virtual List<Address> Addresses { get; set; } = default!;
    public virtual List<CartItem> CartItems { get; set; } = default!;
    public virtual List<Rating> Ratings { get; set; } = default!;
    public virtual List<Role> Roles { get; set; } = default!;
    public virtual List<Product> Products { get; set; } = default!;
}