﻿using E_commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerce.Infrastructure.Persistance;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasMany(p => p.CartItems)
            .WithOne(ci => ci.Product)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.ProductCategories)
            .WithMany(pc => pc.Products);

        builder.HasMany(p => p.Ratings)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.Price)
            .HasPrecision(18, 2);
    }
}
