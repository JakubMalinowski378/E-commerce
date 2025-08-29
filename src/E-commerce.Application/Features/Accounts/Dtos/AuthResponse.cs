namespace E_commerce.Application.Features.Accounts.Dtos;

public class AuthResponse(string accessToken, string refreshToken)
{
    public string AccessToken { get; set; } = accessToken;
    public string RefreshToken { get; set; } = refreshToken;
}