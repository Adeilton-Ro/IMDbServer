using IMDb.Domain.Entities.Abstract;

namespace IMDb.Domain.Entities;
public class Actor : Cast
{
    public IEnumerable<ActorFilm> ActorFilms { get; set; }
}