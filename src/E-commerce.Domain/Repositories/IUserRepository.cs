using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsEmailInUseAsync(string email);
    Task<User?> GetByEmailAsync(
        string email,
        Func<IQueryable<User>, IQueryable<User>>? include = null,
        bool asNoTracking = false);
    Task<User?> GetByConfirmationTokenAsync(string token);
    Task<User?> GetByResetPasswordTokenAsync(string token);
    Task<User?> GetByRefreshTokenAsync(string token);
    Task<bool> IsPhoneNumberInUseAsync(string phoneNumber);
}
