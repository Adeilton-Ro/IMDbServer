using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Infra.DataBase.Mapping;
public class ActorFilmMap : IEntityTypeConfiguration<ActorFilm>
{
    public void Configure(EntityTypeBuilder<ActorFilm> builder)
    {
        builder.HasKey(af => af.Id);
        builder.HasOne(af => af.Actor).WithMany(a => a.ActorFilms).HasForeignKey(af => af.ActorId);
        builder.HasOne(af => af.Film).WithMany(f => f.ActorFilms).HasForeignKey(af => af.FilmId);
    }
}