using IMDb.Domain.Entities.Abstract;

namespace IMDb.Domain.Entities;
public class ActorFilm : Entity
{
    public Guid FilmId { get; set; }
    public Film Film { get; set; }
    public Guid ActorId { get; set; }
    public Actor Actor { get; set; }
}