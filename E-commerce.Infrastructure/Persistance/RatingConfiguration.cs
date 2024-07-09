using E_commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerce.Infrastructure.Persistance;
public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Product)
            .WithMany(p => p.Ratings)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
