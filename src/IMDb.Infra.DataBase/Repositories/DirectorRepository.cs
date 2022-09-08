using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;

namespace IMDb.Infra.DataBase.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly IMDbServerDbContext context;

        public DirectorRepository(IMDbServerDbContext context)
        {
            this.context = context;
        }
        public async Task Create(Director director, CancellationToken cancellationToken)
            => await context.AddAsync(director, cancellationToken);
    }
}
