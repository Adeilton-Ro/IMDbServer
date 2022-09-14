namespace IMDbServer.Api.Endpoints.Film.CustomizerRequests.Rate;
public record RateRequest(Guid FilmId, int Grade);