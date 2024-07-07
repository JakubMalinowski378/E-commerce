using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface IUserRepository                                                    
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<IEnumerable<User>> GetUsersAsync();
    Task<Guid> Create(User user);
    Task<bool> UserExists(string email);
    Task<User?> GetUserByEmailAsync(string email)
    Task<bool> DeleteUserAsync(Guid id);
}
