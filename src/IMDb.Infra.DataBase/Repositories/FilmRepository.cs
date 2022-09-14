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

    public IEnumerable<Film> GetAllByFilters(IEnumerable<Guid?> directors, string? name,
        IEnumerable<Guid?> gender, IEnumerable<Guid?> actors, int start, int end)
    {
        var filters = new List<Func<Film, bool>>();

        var films = context.Films.Include(f => f.FilmImages)
            .Include(f => f.GenderFilm).ThenInclude(gf => gf.Gender)
            .Include(f => f.DirectorFilms).ThenInclude(df => df.Director)
            .Include(f => f.ActorFilms).ThenInclude(af => af.Actor)
            .OrderBy(c => c.Name).Skip(start).Take(end);

        if (!string.IsNullOrEmpty(name))
            filters.Add(film => film.Name.StartsWith(name));

        if (!gender.Any(g => g is null))
            filters.Add(film => film.GenderFilm.Any(gf => gender.Contains(gf.GenderId)));

        if (!directors.Any(d => d is null))
            filters.Add(film => film.DirectorFilms.Any(df => directors.Contains(df.DirectorId)));

        if (!actors.Any(a => a is null))
            filters.Add(film => film.ActorFilms.Any(af => actors.Contains(af.ActorId)));

        return filters.Aggregate(films as IEnumerable<Film>, (seed, filter) => seed.Where(filter));
    }

    public IEnumerable<Film> GetAllByFiltersDescending(IEnumerable<Guid?> directors, string? name,
        IEnumerable<Guid?> gender, IEnumerable<Guid?> actors, int start, int end)
    {
        var filters = new List<Func<Film, bool>>();

        var films = context.Films.Include(f => f.FilmImages).Include(f => f.GenderFilm)
            .Include(f => f.DirectorFilms).Include(f => f.ActorFilms).Include(f => f.Votes)
            .OrderBy(c => c.Name).Skip(start).Take(end);

        if (!string.IsNullOrEmpty(name))
            filters.Add(film => film.Name.StartsWith(name));

        if (gender.Any(g => g is null))
            filters.Add(film => film.GenderFilm.All(gf => gender.Contains(gf.GenderId)));

        if (directors.Any(d => d is null))
            filters.Add(film => film.DirectorFilms.All(df => directors.Contains(df.DirectorId)));

        if (actors.Any(a => a is null))
            filters.Add(film => film.ActorFilms.All(af => actors.Contains(af.ActorId)));

        return filters.Aggregate(films as IEnumerable<Film>, (seed, filter) => seed.Where(filter));
    }
    public Task<Film> GetById(Guid id, CancellationToken cancellationToken)
        => context.Films.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

    public async Task<bool> NameAlredyExist(string name, CancellationToken cancellationToken)
        => await context.Films.AnyAsync(f => f.Name.ToLower() == name.ToLower(), cancellationToken);

    public async Task NewImages(IEnumerable<FilmImage> film, CancellationToken cancellationToken)
        => await context.AddRangeAsync(film, cancellationToken);

    public void Update(Film film)
        => context.Update(film);
}
