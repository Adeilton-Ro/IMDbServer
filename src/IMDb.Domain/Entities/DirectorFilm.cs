using IMDb.Domain.Entities.Abstract;

namespace IMDb.Domain.Entities;
public class DirectorFilm : Entity
{
    public Guid DirectorId { get; set; }
    public Director Director { get; set; }
    public Guid FilmId { get; set; }
    public Film Film { get; set; }
}