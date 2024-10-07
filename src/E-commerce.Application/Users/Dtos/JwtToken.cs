namespace E_commerce.Application.Users.Dtos;

public class JwtToken(string token)
{
    public string Token { get; set; } = token;
}