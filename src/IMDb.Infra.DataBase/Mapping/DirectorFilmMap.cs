using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Infra.DataBase.Mapping;
public class DirectorFilmMap : IEntityTypeConfiguration<DirectorFilm>
{
    public void Configure(EntityTypeBuilder<DirectorFilm> builder)
    {
        builder.HasKey(df => df.Id);
        builder.HasOne(df => df.Director).WithMany(d => d.DirectorFilms).HasForeignKey(df => df.DirectorId);
        builder.HasOne(df => df.Film).WithMany(f => f.DirectorFilms).HasForeignKey(df => df.FilmId);
    }
}