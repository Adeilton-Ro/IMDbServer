using IMDb.Domain.Entities.Abstract;

namespace IMDb.Domain.Entities;
public class Client : User
{
    public IEnumerable<Vote> Votes { get; set; }
}