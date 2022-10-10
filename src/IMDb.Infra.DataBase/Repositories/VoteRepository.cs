using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Infra.DataBase.Repositories;
public class VoteRepository : IVoteRepository
{
	private readonly IMDbServerDbContext context;

	public VoteRepository(IMDbServerDbContext context)
	{
		this.context = context;
	}

	public async Task Create(Vote vote, CancellationToken cancellationToken)
		=> await context.AddAsync(vote, cancellationToken);

	public Task<bool> IsAlredyRated(Guid clientId, Guid filmId, CancellationToken cancellationToken)
		=> context.Votes.AnyAsync(v => v.ClientId == clientId && v.FilmId == filmId, cancellationToken);
}