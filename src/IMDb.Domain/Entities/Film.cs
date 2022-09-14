using IMDb.Domain.Entities.Abstract;

namespace IMDb.Domain.Entities;
public class Film : Entity
{
    public string Name { get; set; } = string.Empty;
    public decimal Average { get; set; } = 0;
    public int Voters { get; set; } = 0;
    public string Synopsis { get; set; } = string.Empty;
    public IEnumerable<FilmImage> FilmImages { get; set; }
    public IEnumerable<GenderFilm> GenderFilm { get; set; }
    public IEnumerable<ActorFilm> ActorFilms { get; set; }
    public IEnumerable<DirectorFilm> DirectorFilms { get; set; }
    public IEnumerable<Vote> Votes { get; set; }
    public DateTime Release { get; set; }
}