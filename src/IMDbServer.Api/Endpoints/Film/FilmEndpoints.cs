using AutoMapper;
using IMDb.Application.Features.Film.GetDirectors;
using IMDb.Application.Features.Film.NewActor;
using IMDb.Application.Features.Film.NewDirector;
using IMDb.Application.Features.Film.NewGender;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewActor;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewDirector;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDbServer.Api.Endpoints.Film;
public static class FilmEndpoints
{
    public static WebApplication UseFilmEndpoints(this WebApplication app)
    {
        app.MapPost("film/newdirector", [Authorize(Roles = "Adm" )] async ([FromServices] ISender sender, [FromServices] IMapper mapper
            , HttpRequest request, CancellationToken cancellationToken) =>
        {
            var form = await request.ReadFormAsync(cancellationToken);
            var file = request.Form.Files;

            var newDirectorRequest = new NewDirectorRequest(form["name"], file[0]);

            var map = mapper.Map<NewDirectorCommand>(newDirectorRequest);
            var result = await sender.Send(map, cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        }).Accepts<NewDirectorRequest>("multipart/form-data");

        app.MapPost("film/newgender", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, NewGenderCommand request, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });

        app.MapPost("film/newactor", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, [FromServices] IMapper mapper
            , HttpRequest request, CancellationToken cancellationToken) =>
        {
            var form = await request.ReadFormAsync(cancellationToken);
            var file = request.Form.Files;

            var newActorRequest = new NewActorRequest(form["name"], file[0]);

            var map = mapper.Map<NewActorCommand>(newActorRequest);
            var result = await sender.Send(map, cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        }).Accepts<NewActorRequest>("multipart/form-data");

        app.MapGet("film/directors", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetDirectorQuery(), cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });

        return app;
    }
}
