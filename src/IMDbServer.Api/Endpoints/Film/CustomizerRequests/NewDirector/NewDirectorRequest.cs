namespace IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewDirector;
public record NewDirectorRequest(string Name, string Description, IFormFile Images);