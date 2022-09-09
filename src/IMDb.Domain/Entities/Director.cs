using IMDb.Domain.Entities.Abstract;

namespace IMDb.Domain.Entities;
public class Director : Cast
{
    public IEnumerable<DirectorFilm> DirectorFilms { get; set; }
}