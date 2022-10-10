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
        builder.Property(c => c.isActive).HasDefaultValue(true);

        builder.HasData(
            new Adm
            {
                Id = Guid.Parse("026a029c-ca06-4094-8410-7a249ebbb340"),
                Name = "Adm",
                Email = "adm@imdbserver.com",
                Hash = "2e8ab8b0879f61aff2efdf0b7303ff059daafb3a812167d19735534f83b21235",
                Salt = "u/UxV85TWfn3DQ=="
            }
            );
    }
}