using IMDb.Domain.Entities.Abstract;

namespace IMDb.Domain.Entities;
public class GenderFilm : Entity
{
    public Guid GenderId { get; set; }
    public Gender Gender { get; set; }
    public Guid FilmId { get; set; }
    public Film Film { get; set; }
}