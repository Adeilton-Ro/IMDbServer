using IMDb.Domain.Entities.Abstract;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Infra.DataBase.Repositories
{
    public class CastRepository<T> : ICastRepository<T> where T : Cast
    {
        private readonly IMDbServerDbContext context;

        public CastRepository(IMDbServerDbContext context)
        {
            this.context = context;
        }

        public Task<bool> AreAlredyCreated(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => context.Set<T>().AnyAsync(c => ids.Contains(c.Id), cancellationToken);

        public async Task Create(T cast, CancellationToken cancellationToken)
            => await context.Set<T>().AddAsync(cast, cancellationToken);

        public IEnumerable<T> GetAll()
            => context.Set<T>();
    }
}
