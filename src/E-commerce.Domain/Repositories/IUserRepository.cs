using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> UserExists(string email);
    Task<User?> GetByEmail(
        string email,
        Func<IQueryable<User>, IQueryable<User>>? include = null,
        bool asNoTracking = false);
    Task<User?> GetByConfirmationTokenAsync(string token);
    Task<User?> GetByResetPasswordTokenAsync(string token);
    Task<bool> IsPhoneNumberInUse(string phoneNumber);
}
