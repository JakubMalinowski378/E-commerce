namespace E_commerce.Domain.Interfaces;

public interface IPermissionService
{
    bool HasPermission(string role, IUserOwned resource, string action);
}
