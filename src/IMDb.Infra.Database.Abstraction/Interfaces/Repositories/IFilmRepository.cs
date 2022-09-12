using IMDb.Domain.Entities;

namespace IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
public interface IFilmRepository
{
    Task<bool> NameAlredyExist(string name, CancellationToken cancellationToken);
    Task Create(Film film, CancellationToken cancellationToken);
    Task<Film> GetById(Guid id, CancellationToken cancellationToken);
    Task NewImages(IEnumerable<FilmImage> filmImage, CancellationToken cancellationToken);
}