using IMDb.Domain.Entities;

namespace IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
public interface IGenderRepository
{
    Task Create(Gender gender, CancellationToken cancellationToken);
    IEnumerable<Gender> GetAll();
    Task<bool> AreAlredyCreated(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}
