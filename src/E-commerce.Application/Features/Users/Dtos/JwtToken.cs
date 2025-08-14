namespace E_commerce.Application.Features.Users.Dtos;

public class JwtToken(string token)
{
    public string Token { get; set; } = token;
}