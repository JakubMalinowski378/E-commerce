using E_commerce.Application.Features.Users;

namespace E_commerce.Application.Interfaces;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}
