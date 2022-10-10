using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Infra.DataBase.Mapping;
public class FilmImagesMap : IEntityTypeConfiguration<FilmImage>
{
    public void Configure(EntityTypeBuilder<FilmImage> builder)
    {
        builder.HasKey(fi => fi.Id);
        builder.Property(fi => fi.Uri).IsRequired();
        builder.HasOne(fi => fi.Film).WithMany(f => f.FilmImages).HasForeignKey(fi => fi.FilmId);
    }
}