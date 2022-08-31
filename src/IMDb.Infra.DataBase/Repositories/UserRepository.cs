using IMDb.Domain.Entities.Abstract;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Infra.DataBase.Repositories;
public class UserRepository<T> : IUserRepository<T> where T : User
{
    private readonly IMDbServerDbContext context;

    public UserRepository(IMDbServerDbContext context)
    {
        this.context = context;

    }
    public async Task Create(T user, CancellationToken cancellationToken)
        => await context.AddAsync(user, cancellationToken);

    public Task<T> GetByEmail(string email, CancellationToken cancellationToken)
     => context.Set<T>().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
        => context.Set<T>().AnyAsync(c => c.Email == email, cancellationToken);
}