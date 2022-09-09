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

    public async Task<bool> NameAlredyExist(string name, CancellationToken cancellationToken)
        => await context.Films.AnyAsync(f => f.Name == name, cancellationToken);
}
