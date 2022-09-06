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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AdmMap());
		modelBuilder.ApplyConfiguration(new ClientMap());
		modelBuilder.ApplyConfiguration(new FilmsMap());
        modelBuilder.ApplyConfiguration(new ActorsMap());
        modelBuilder.ApplyConfiguration(new GenderMap());
        modelBuilder.ApplyConfiguration(new DirectorMap());
		modelBuilder.ApplyConfiguration(new VoteMap());
    }
}
