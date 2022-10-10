using IMDb.Domain.Entities.Abstract;

namespace IMDb.Domain.Entities;
public class Gender : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<GenderFilm> GenderFilms { get; set; }
}