using FluentResults;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Films.GetActor;
public class GetActorQueryHandler : IRequestHandler<GetActorQuery, Result<IEnumerable<GetActorQueryResponse>>>
{
    private readonly ICastRepository<Actor> actorRepository;

    public GetActorQueryHandler(ICastRepository<Actor> actorRepository)
    {
        this.actorRepository = actorRepository;
    }
    public Task<Result<IEnumerable<GetActorQueryResponse>>> Handle(GetActorQuery request, CancellationToken cancellationToken)
    {
        var response = actorRepository.GetAll().Select(a => new GetActorQueryResponse(a.Id, a.Name, a.UrlImage));
        return Task.FromResult(Result.Ok(response));
    }
}
