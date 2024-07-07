using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<User?> GetUserByIdAsync(Guid id, params Expression<Func<User, object>>[] includePredicates)
    {
        var query = ApplyIncludes(includePredicates);
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetUserByEmailAsync(string email, params Expression<Func<User, object>>[] includePredicates)
    {
        var query = ApplyIncludes(includePredicates);
        return await query.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IEnumerable<User>> GetUsersAsync(params Expression<Func<User, object>>[] includePredicates)
    {
        var query = ApplyIncludes(includePredicates);
        return await query.ToListAsync();
    }

    public async Task<bool> UserExists(string email)
        => await _dbContext.Users.AnyAsync(x => x.Email == email);

    private IQueryable<User> ApplyIncludes(params Expression<Func<User, object>>[] includePredicates)
    {
        var query = _dbContext.Users.AsQueryable();
        foreach (var includePredicate in includePredicates)
            query = query.Include(includePredicate);
        return query;
    }
}
