namespace E_commerce.Application.Users.Dtos;

public class UserDto
{
    public string Firstname { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public char Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
