namespace E_commerce.Application.Users;
public record CurrentUser(Guid Id,
    string Email,
    IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
