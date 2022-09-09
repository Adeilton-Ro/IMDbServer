using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Film.GetActor;
public record GetActorQuery() : IRequest<Result<IEnumerable<GetActorQueryResponse>>>;