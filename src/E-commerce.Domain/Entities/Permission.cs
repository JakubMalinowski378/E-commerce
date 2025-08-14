namespace E_commerce.Domain.Entities;

public class Permission
{
    public Guid Id { get; set; }
    public string Resource { get; set; } = default!;
    public string Action { get; set; } = default!;
    public List<Role> Roles { get; set; } = default!;
}