using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Infra.DataBase.Mapping;
public class AdmMap : IEntityTypeConfiguration<Adm>
{
    public void Configure(EntityTypeBuilder<Adm> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Salt).IsRequired().HasMaxLength(16);
        builder.Property(c => c.Hash).IsRequired().HasMaxLength(64);
    }
}