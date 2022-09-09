using FluentResults;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Film.GetDirectors;
public class GetDirectorQueryHandler : IRequestHandler<GetDirectorQuery, Result<IEnumerable<GetDirectorQueryResponse>>>
{
    private readonly ICastRepository<Director> directorRepository;

    public GetDirectorQueryHandler(ICastRepository<Director> directorRepository)
    {
        this.directorRepository = directorRepository;
    }
    public async Task<Result<IEnumerable<GetDirectorQueryResponse>>> Handle(GetDirectorQuery request, CancellationToken cancellationToken)
    {
        var response = directorRepository.GetAll().Select(d => new GetDirectorQueryResponse(d.Id, d.Name, d.UrlImage));
        return Result.Ok(response);
    }
}