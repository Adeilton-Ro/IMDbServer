using IMDb.Domain.Entities;
using IMDb.Domain.Entities.Abstract;

namespace IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
public interface ICastRepository<T> where T : Cast
{
    Task Create(T cast, CancellationToken cancellationToken);
    IEnumerable<T> GetAll();
    Task<bool> AreAlredyCreated(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}