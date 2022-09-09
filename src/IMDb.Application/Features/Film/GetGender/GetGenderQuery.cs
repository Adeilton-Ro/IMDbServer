using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Film.GetGender;
public record GetGenderQuery() : IRequest<Result<IEnumerable<GetGenderQueryResponse>>>;