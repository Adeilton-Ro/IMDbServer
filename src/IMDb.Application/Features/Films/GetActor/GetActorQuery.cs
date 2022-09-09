using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Films.GetActor;
public record GetActorQuery() : IRequest<Result<IEnumerable<GetActorQueryResponse>>>;