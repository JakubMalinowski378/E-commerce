using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_commerce.Infrastructure.Repositories;
public class UserRepository(EcommerceDbContext dbContext,
    IRolesRepository rolesRepository)
    : IUserRepository
{
    private readonly EcommerceDbContext _dbContext = dbContext;
    private readonly IRolesRepository _rolesRepository = rolesRepository;

    public async Task<Guid> Create(User user)
    {
        var role = await _rolesRepository.GetRole(UserRoles.User);
        user.Roles = new List<Role> { role };
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

    public async Task DeleteUser(User user)
    {
        foreach (var product in user.Products)
        {
            await dbContext.Entry(product).Collection(p => p.CartItems).LoadAsync();
            await dbContext.Entry(product).Collection(p => p.Ratings).LoadAsync();

            dbContext.CartItems.RemoveRange(product.CartItems);
            dbContext.Ratings.RemoveRange(product.Ratings);
        }
        dbContext.CartItems.RemoveRange(user.CartItems);
        dbContext.Ratings.RemoveRange(user.Ratings);
        dbContext.Products.RemoveRange(user.Products);

        dbContext.Users.Remove(user);

        await dbContext.SaveChangesAsync();
    }



    private IQueryable<User> ApplyIncludes(params Expression<Func<User, object>>[] includePredicates)
    {
        var query = _dbContext.Users.AsQueryable();
        foreach (var includePredicate in includePredicates)
            query = query.Include(includePredicate);
        return query;
    }

    public async Task<IEnumerable<Rating>?> GetAllRatingsOfUser(Guid id)
    {
        var user = await _dbContext.Users.Include(x => x.Ratings).FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            return null;
        }
        return user.Ratings;
    }

    public async Task<User?> GetUserByConfirmationTokenAsync(string token)
    => await _dbContext.Users.FirstOrDefaultAsync(x => x.ConfirmationToken == token);

    public async Task SaveUserAsync()
    => await _dbContext.SaveChangesAsync();
}
