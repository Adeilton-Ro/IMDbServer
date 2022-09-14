namespace IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewActor;
public record NewActorRequest(string Name, string Description, IFormFile Images);