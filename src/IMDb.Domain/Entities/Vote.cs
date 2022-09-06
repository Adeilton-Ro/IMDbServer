using IMDb.Domain.Entities.Abstract;

namespace IMDb.Domain.Entities;
public class Vote : Entity
{
    public Guid ClientId { get; set; }
    public Client Client { get; set; }
    public Guid FilmId { get; set; }
    public Film Film { get; set; }
    public int Evaluation { get; set; }
}