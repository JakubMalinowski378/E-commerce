using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;

public class UserRepository(
    ECommerceDbContext dbContext)
    : Repository<User>(dbContext), IUserRepository
{
    public async Task<bool> IsEmailInUseAsync(string email)
        => await _dbSet.AnyAsync(x => x.Email == email);

    public async Task<User?> GetByEmailAsync(
        string email,
        Func<IQueryable<User>, IQueryable<User>>? include = null,
        bool asNoTracking = false)
    {
        var query = _dbSet.AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        if (include is not null)
            query = include(query);

        return await query.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByConfirmationTokenAsync(string token)
        => await _dbSet.FirstOrDefaultAsync(x => x.ConfirmationToken == token);

    public async Task<User?> GetByResetPasswordTokenAsync(string token)
        => await _dbSet.SingleOrDefaultAsync(x => x.ResetPasswordToken == token);

    public async Task<bool> IsPhoneNumberInUseAsync(string phoneNumber)
        => await _dbSet.AnyAsync(x => x.PhoneNumber == phoneNumber);

    public async Task<User?> GetByRefreshTokenAsync(string token)
        => await _dbSet.FirstOrDefaultAsync(x => x.RefreshToken == token);
}
