using E_commerce.Application.Users;

namespace E_commerce.Application.Interfaces;
public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}
