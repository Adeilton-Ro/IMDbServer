using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Films.NewFilm;
public record NewFilmCommand : IRequest<Result<NewFilmCommandResponse>>
{
    public string Name { get; set; }
    public IEnumerable<Guid> Directors { get; set; }
    public IEnumerable<Guid> Actors { get; set; }
    public IEnumerable<Guid> Genders { get; set; }

    public NewFilmCommand() { }
    public NewFilmCommand(string name, IEnumerable<Guid> directors, IEnumerable<Guid> actors, IEnumerable<Guid> genders)
    {
        Name = name;
        Directors = directors;
        Actors = actors;
        Genders = genders;
    }
}