using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;
public class UserRepository(EcommerceDbContext dbContext) : IUserRepository
{
    private readonly EcommerceDbContext _dbContext = dbContext;
    public async Task<Guid> Create(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
        => await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<User?> GetUserByEmailAsync(string email)
        => await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<IEnumerable<User>> GetUsersAsync()
        => await _dbContext.Users.ToListAsync();

    public async Task<bool> UserExists(string email)
        => await _dbContext.Users.AnyAsync(x => x.Email == email);
}
