using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;

namespace IMDb.Infra.DataBase.Repositories;
public class ActorRepository : IActorRepository
{
    private readonly IMDbServerDbContext context;

    public ActorRepository(IMDbServerDbContext context)
    {
        this.context = context;
    }
    public async Task Create(Actor actor, CancellationToken cancellationToken)
        => await context.AddAsync(actor, cancellationToken);
}