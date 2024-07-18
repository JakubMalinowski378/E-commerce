using E_commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerce.Infrastructure.Persistance;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(u => u.PhoneNumber)
            .IsUnique();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users);

        builder.HasIndex(u => u.ResetPasswordToken)
            .IsUnique();

        builder.HasIndex(u => u.ConfirmationToken)
            .IsUnique();
    }
}
