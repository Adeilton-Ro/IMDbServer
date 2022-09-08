using AutoMapper;
using IMDb.Application.Features.Film.NewDirector;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewDirector;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMDbServer.Api.Endpoints.Film;
public static class FilmEndpoints
{
    public static WebApplication UseFilmEndpoints(this WebApplication app)
    {
        app.MapPost("film/newdirector", async ([FromServices] ISender sender, [FromServices] IMapper mapper
            , HttpRequest request, CancellationToken cancellationToken) =>
        {
            var form = await request.ReadFormAsync(cancellationToken);
            var file = request.Form.Files;

            var newDirectorRequest = new NewDirectorRequest(form["name"], file.First());

            var map = mapper.Map<NewDirectorCommand>(newDirectorRequest);
            var result = await sender.Send(map, cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        }).Accepts<NewDirectorRequest>("multipart/form-data");
        return app;
    }
}
