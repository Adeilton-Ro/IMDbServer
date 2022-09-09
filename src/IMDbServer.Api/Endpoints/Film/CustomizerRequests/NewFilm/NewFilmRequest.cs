namespace IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewFilm;
public record NewFilmRequest(string Name, IFormFileCollection Images,
    IEnumerable<Guid> Directors, IEnumerable<Guid> Actors, IEnumerable<Guid> Genders);