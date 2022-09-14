using FluentResults;
using IMDb.Application.Commun;
using MediatR;

namespace IMDb.Application.Features.Films.Rate;
public record RateCommand : IRequest<Result<RateCommandResponse>>
{
    [FromUserInfo]
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public int Grade { get; set; }
    public RateCommand() { }
    public RateCommand(Guid id, Guid filmId, int grade)
    {
        Id = id;
        FilmId = filmId;
        Grade = grade;
    }
}