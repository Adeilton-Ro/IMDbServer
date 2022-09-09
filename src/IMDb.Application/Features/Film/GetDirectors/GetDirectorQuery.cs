using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Film.GetDirectors;
public record GetDirectorQuery() : IRequest<Result<IEnumerable<GetDirectorQueryResponse>>>;