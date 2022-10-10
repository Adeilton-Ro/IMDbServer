using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Infra.DataBase.Mapping;
public class ClientMap : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Salt).IsRequired().HasMaxLength(16);
        builder.Property(c => c.Hash).IsRequired().HasMaxLength(64);
        builder.Property(c => c.isActive).HasDefaultValue(true);
        builder.HasMany(c => c.Votes).WithOne(v => v.Client).HasForeignKey(v => v.ClientId);
    }
}