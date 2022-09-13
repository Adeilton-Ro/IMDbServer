using FluentResults;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Listing.GetFilms;
public class GetFilmsQueryHandler : IRequestHandler<GetFilmsQuery, Result<IEnumerable<GetFilmsQueryResponse>>>
{
    private readonly IFilmRepository filmRepository;

    public GetFilmsQueryHandler(IFilmRepository filmRepository)
    {
        this.filmRepository = filmRepository;
    }
    public async Task<Result<IEnumerable<GetFilmsQueryResponse>>> Handle(GetFilmsQuery request, CancellationToken cancellationToken)
    {
        var start = request.QuantityOfItems * (request.Page - 1);
        var end = start + request.QuantityOfItems;
        end = end > 0 ? end : int.MaxValue;

        List<Film> films;

        if (request.IsDescending)
            films = filmRepository.GetAllByFiltersDescending(request.Directors, request.Name, request.Gender, request.Actors, start, end).ToList();
        else
            films = filmRepository.GetAllByFilters(request.Directors, request.Name, request.Gender, request.Actors, start, end).ToList();

        var response = films.Select(s => 
            new GetFilmsQueryResponse(s.Id, s.Name, s.FilmImages.Select(fi => fi.Uri), s.Release,
            s.GenderFilm.Select(g => new GetGendersFilms(g.GenderId, g.Gender.Name, g.Gender.Description)),
            s.DirectorFilms.Select(d => new GetCastFilms(d.DirectorId, d.Director.Name, d.Director.UrlImage, d.Director.Birthday, d.Director.Description)),
            s.ActorFilms.Select(a => new GetCastFilms(a.ActorId, a.Actor.Name, a.Actor.UrlImage, a.Actor.Birthday, a.Actor.Description)), 
            s.Average));

        return Result.Ok(response);
    }
}