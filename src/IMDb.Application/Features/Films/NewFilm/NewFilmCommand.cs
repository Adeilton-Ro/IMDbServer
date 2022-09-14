using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Films.NewFilm;
public record NewFilmCommand(string Name, DateTime Release, IEnumerable<Guid> Directors, 
    IEnumerable<Guid> Actors, IEnumerable<Guid> Genders) : IRequest<Result<NewFilmCommandResponse>>;