using Microsoft.EntityFrameworkCore;
using E_commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_commerce.Infrastructure.Persistance;
public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {

    }
}