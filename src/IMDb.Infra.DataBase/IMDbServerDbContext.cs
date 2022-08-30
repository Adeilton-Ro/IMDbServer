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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AdmMap());
		modelBuilder.ApplyConfiguration(new ClientMap());
	}
}
