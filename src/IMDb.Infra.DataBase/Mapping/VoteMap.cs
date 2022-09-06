using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Infra.DataBase.Mapping;
public class VoteMap : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Evaluation).IsRequired();
        builder.HasOne(v => v.Client).WithMany(c => c.Votes).HasForeignKey(v => v.ClientId);
        builder.HasOne(v => v.Film).WithMany(f => f.Votes).HasForeignKey(v => v.FilmId);
    }
}