using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Infra.DataBase.Mapping;
public class GenderMap : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Name).IsRequired();
        builder.HasMany(g => g.Films).WithOne(f => f.Gender).HasForeignKey(f => f.GenderId);
    }
}