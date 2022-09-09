using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.DataBase.Mapping;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Infra.DataBase;
public class IMDbServerDbContext : DbContext, IUnitOfWork
{
	public IMDbServerDbContext(DbContextOptions<IMDbServerDbContext> options) : base(options) { }

	public DbSet<Client> Clients { get; set; }
	public DbSet<Adm> Adms { get; set; }
	public DbSet<Film> Films { get; set; }
	public DbSet<Actor> Actors { get; set; }
	public DbSet<Gender> Genders { get; set; }
	public DbSet<Director> Directors { get; set; }
	public DbSet<Vote> Votes { get; set; }
	public DbSet<DirectorFilm> DirectorFilms { get; set; }
	public DbSet<GenderFilm> GenderFilms { get; set; }
	public DbSet<ActorFilm> ActorFilms { get; set; }
	public DbSet<FilmImage> FilmImages { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AdmMap());
		modelBuilder.ApplyConfiguration(new ClientMap());
		modelBuilder.ApplyConfiguration(new FilmsMap());
        modelBuilder.ApplyConfiguration(new ActorsMap());
        modelBuilder.ApplyConfiguration(new GenderMap());
        modelBuilder.ApplyConfiguration(new DirectorMap());
		modelBuilder.ApplyConfiguration(new VoteMap());
		modelBuilder.ApplyConfiguration(new DirectorFilmMap());
		modelBuilder.ApplyConfiguration(new GenderFilmMap());
		modelBuilder.ApplyConfiguration(new ActorFilmMap());
		modelBuilder.ApplyConfiguration(new FilmImagesMap());
    }
}
