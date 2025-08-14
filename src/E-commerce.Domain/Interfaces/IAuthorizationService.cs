namespace E_commerce.Domain.Interfaces;

public interface IAuthorizationService
{
    Task<bool> HasPermission(IUserOwned resource, string action);
}
