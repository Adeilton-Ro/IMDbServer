using IMDb.Domain.Entities;
using IMDb.Domain.Entities.Abstract;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;

namespace IMDb.Infra.DataBase.Repositories
{
    public class CastRepository<T> : ICastRepository<T> where T : Cast
    {
        private readonly IMDbServerDbContext context;

        public CastRepository(IMDbServerDbContext context)
        {
            this.context = context;
        }

        public async Task Create(T cast, CancellationToken cancellationToken)
            => await context.Set<T>().AddAsync(cast, cancellationToken);

        public IEnumerable<T> GetAll()
            => context.Set<T>();
    }
}
