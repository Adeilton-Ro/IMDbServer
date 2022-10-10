using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Films.GetDirectors;
public record GetDirectorQuery() : IRequest<Result<IEnumerable<GetDirectorQueryResponse>>>;