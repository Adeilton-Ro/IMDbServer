using IMDb.Domain.Entities.Abstract;

namespace IMDb.Domain.Entities;
public class Film : Entity
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<FilmImage> FilmImages { get; set; }
    public Guid GenderId { get; set; }
    public Gender Gender { get; set; }
    public IEnumerable<ActorFilm> ActorFilms { get; set; } 
    public Guid DirectorId { get; set; }
    public Director Director { get; set; }
    public IEnumerable<Vote> Votes { get; set; }
}