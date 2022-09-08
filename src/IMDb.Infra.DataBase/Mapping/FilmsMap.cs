using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Infra.DataBase.Mapping;
internal class FilmsMap : IEntityTypeConfiguration<Film>
{
    public void Configure(EntityTypeBuilder<Film> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(X => X.Name).IsRequired();
        builder.HasMany(f => f.Votes).WithOne(v => v.Film).HasForeignKey(v => v.FilmId);
        builder.HasOne(f => f.Director).WithMany(d => d.Films).HasForeignKey(f => f.DirectorId);
        builder.HasMany(f => f.ActorFilms).WithOne(af => af.Film).HasForeignKey(af => af.FilmId);
        builder.HasOne(f => f.Gender).WithMany(g => g.Films).HasForeignKey(f => f.GenderId);
        builder.HasMany(f => f.FilmImages).WithOne(fi => fi.Film).HasForeignKey(fi => fi.FilmId);
    }
}