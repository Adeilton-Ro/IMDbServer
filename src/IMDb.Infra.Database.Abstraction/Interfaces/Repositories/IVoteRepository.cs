using IMDb.Domain.Entities;

namespace IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
public interface IVoteRepository
{
    Task Create(Vote vote, CancellationToken cancellationToken);
    Task<bool> IsAlredyRated(Guid ClientId, Guid FilmId,CancellationToken cancellationToken);
}