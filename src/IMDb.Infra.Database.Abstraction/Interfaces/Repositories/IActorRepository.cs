using IMDb.Domain.Entities;

namespace IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
public interface IActorRepository
{
    Task Create(Actor actor, CancellationToken cancellationToken);
}