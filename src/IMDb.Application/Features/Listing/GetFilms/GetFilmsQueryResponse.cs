namespace IMDb.Application.Features.Listing.GetFilms;
public record GetFilmsQueryResponse(Guid Id, string Name, IEnumerable<string> ImageUrls, DateTime Release, IEnumerable<GetGendersFilms> Genders, IEnumerable<GetCastFilms> Diretors, IEnumerable<GetCastFilms> Actors, decimal Avarage);

public record GetGendersFilms(Guid Id, string Name, string Description);
public record GetCastFilms(Guid Id, string Name, string ImageUrl, DateTime Birthday, string Description);