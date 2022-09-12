using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Infra.DataBase.Repositories;
public class FilmRepository : IFilmRepository
{
    private readonly IMDbServerDbContext context;

    public FilmRepository(IMDbServerDbContext context)
    {
        this.context = context;
    }
    public async Task Create(Film film, CancellationToken cancellationToken)
        => await context.AddAsync(film, cancellationToken);

    public Task<Film> GetById(Guid id, CancellationToken cancellationToken)
        => context.Films.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

    public async Task<bool> NameAlredyExist(string name, CancellationToken cancellationToken)
        => await context.Films.AnyAsync(f => f.Name == name, cancellationToken);

    public async Task NewImages(IEnumerable<FilmImage> film, CancellationToken cancellationToken)
        => await context.AddRangeAsync(film, cancellationToken);
}
