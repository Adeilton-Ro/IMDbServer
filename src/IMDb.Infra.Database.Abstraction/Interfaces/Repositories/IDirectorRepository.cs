using IMDb.Domain.Entities;

namespace IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
public interface IDirectorRepository
{
    Task Create(Director director, CancellationToken cancellationToken);
}