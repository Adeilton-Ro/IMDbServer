using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Films.GetGender;
public record GetGenderQuery() : IRequest<Result<IEnumerable<GetGenderQueryResponse>>>;