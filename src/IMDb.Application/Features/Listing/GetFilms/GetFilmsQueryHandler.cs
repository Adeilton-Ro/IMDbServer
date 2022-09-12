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
            films = filmRepository.GetAllByFiltersDescending(request.Directors, request.Name, request.Gender, request.Actors, start, end) ;
        else
            films = filmRepository.GetAllByFilters(request.Directors, request.Name, request.Gender, request.Actors, start, end);
    }
}