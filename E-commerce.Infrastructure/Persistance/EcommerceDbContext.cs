﻿using E_commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Persistance;
public class EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    //public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(x => x.Addresses)
            .WithOne(x => x.User);
        builder.Entity<Cart>()
            .HasOne(x => x.User)
            .WithOne(x => x.Cart)
            .HasForeignKey<Cart>(x => x.UserId);
            

    }

}
