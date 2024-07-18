using E_commerce.Domain.Entities;
using System.Linq.Expressions;

namespace E_commerce.Domain.Repositories;
public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id, params Expression<Func<User, object>>[] includePredicates);
    Task<IEnumerable<User>> GetUsersAsync(params Expression<Func<User, object>>[] includePredicates);
    Task<Guid> Create(User user);
    Task<bool> UserExists(string email);
    Task<User?> GetUserByEmailAsync(string email, params Expression<Func<User, object>>[] includePredicates);
    Task DeleteUser(User user);
    Task SaveUserAsync();
    Task<IEnumerable<Rating>?> GetAllRatingsOfUser(Guid id);
    Task<User?> GetUserByConfirmationTokenAsync(string token);
    Task<User?> GetUserByResetPasswordTokenAsync(string token);

}
