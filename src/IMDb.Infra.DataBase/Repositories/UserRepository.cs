using IMDb.Domain.Commun;
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

    public async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
        => !await context.Set<T>().AnyAsync(c => c.Email == email, cancellationToken);

    public void Edit(T user)
    => context.Set<T>().Update(user);   

    public Task<T> GetById(Guid id, CancellationToken cancellationToken)
        => context.Set<T>().FirstOrDefaultAsync(u => u.Id == id);

    public IEnumerable<T> GetAllActive(PaginatedQueryOptions paginatedQueryOptions)
        => context.Set<T>().PaginateAndOrder(paginatedQueryOptions, u => u.Name);
}