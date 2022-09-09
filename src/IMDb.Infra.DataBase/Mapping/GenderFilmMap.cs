using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Infra.DataBase.Mapping;
public class GenderFilmMap : IEntityTypeConfiguration<GenderFilm>
{
    public void Configure(EntityTypeBuilder<GenderFilm> builder)
    {
        builder.HasKey(gf => gf.Id);
        builder.HasOne(gf => gf.Gender).WithMany(g => g.GenderFilms).HasForeignKey(gf => gf.GenderId);
        builder.HasOne(gf => gf.Film).WithMany(f => f.GenderFilm).HasForeignKey(gf => gf.FilmId);
    }
}