using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Infra.DataBase.Repositories;
public class GenderRepository : IGenderRepository
{
    private readonly IMDbServerDbContext context;

    public GenderRepository(IMDbServerDbContext context)
    {
        this.context = context;
    }

    public Task<bool> AreAlredyCreated(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        => context.Genders.AnyAsync(g => ids.Contains(g.Id), cancellationToken);

    public async Task Create(Gender gender, CancellationToken cancellationToken)
        => await context.AddAsync(gender, cancellationToken);

    public IEnumerable<Gender> GetAll()
        => context.Genders;
}
