namespace IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewFilmsImage;
public record NewFilmsImagesRequest(Guid Id, IFormFileCollection Images);