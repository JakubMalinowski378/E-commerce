﻿namespace E_commerce.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public char Gender { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public byte[] PaswordHash { get; set; }
    public byte[] PasswordSalt { get; set; } 
    public bool EmailConfirmed { get; set; }
    public List<Address> Addresses { get; set; } = default!;
}
