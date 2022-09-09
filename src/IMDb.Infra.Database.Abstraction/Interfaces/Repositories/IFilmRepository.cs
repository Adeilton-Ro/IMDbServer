using IMDb.Domain.Entities;

namespace IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
public interface IFilmRepository
{
    Task<bool> NameAlredyExist(string name, CancellationToken cancellationToken);
    Task Create(Film film, CancellationToken cancellationToken);
}