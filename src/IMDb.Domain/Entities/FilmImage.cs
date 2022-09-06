using IMDb.Domain.Entities.Abstract;

namespace IMDb.Domain.Entities;
public class FilmImage : Entity
{
    public string Uri { get; set; } = string.Empty;
    public Guid FilmId { get; set; }
    public Film Film { get; set; }
}