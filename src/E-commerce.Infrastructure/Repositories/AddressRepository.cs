﻿using E_commerce.Domain.Entities;
using E_commerce.Domain.Repositories;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories;
public class AddressRepository(EcommerceDbContext dbContext) : IAddressRepository
{
    private readonly EcommerceDbContext _dbContext = dbContext;

    public async Task Create(Address address)
    {
        _dbContext.Addresses.Add(address);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Address address)
    {
        _dbContext.Addresses.Remove(address);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Address?> GetByIdAsync(Guid id)
        => await _dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == id);

    public async Task<IEnumerable<Address>> GetUserAddressesAsync(Guid userId)
        => await _dbContext.Addresses.Where(a => a.UserId == userId).ToListAsync();

    public Task SaveChanges()
        => _dbContext.SaveChangesAsync();
}
