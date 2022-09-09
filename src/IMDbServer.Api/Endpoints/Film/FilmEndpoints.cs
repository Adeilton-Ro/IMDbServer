using AutoMapper;
using IMDb.Application.Features.Films.GetActor;
using IMDb.Application.Features.Films.GetDirectors;
using IMDb.Application.Features.Films.GetGender;
using IMDb.Application.Features.Films.NewActor;
using IMDb.Application.Features.Films.NewDirector;
using IMDb.Application.Features.Films.NewFilm;
using IMDb.Application.Features.Films.NewGender;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewActor;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewDirector;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewFilm;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDbServer.Api.Endpoints.Film;
public static class FilmEndpoints
{
    public static WebApplication UseFilmEndpoints(this WebApplication app)
    {
        app.MapPost("film/newdirector", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, [FromServices] IMapper mapper
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

        app.MapGet("film/actors", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetActorQuery(), cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });

        app.MapGet("film/genders", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetGenderQuery(), cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });

        app.MapPost("film/newfilm", [Authorize(Roles = "Adm")] 
        async ([FromServices] ISender sender, [FromServices] IMapper mapper
            , HttpRequest request, CancellationToken cancellationToken) =>
        {
            var form = await request.ReadFormAsync(cancellationToken);
            var file = request.Form.Files;

            var directors = form["directors"].Select(d => Guid.Parse(d));
            var actors = form["actors"].Select(a => Guid.Parse(a));
            var genders = form["genders"].Select(g => Guid.Parse(g));

            var newFilmRequest = new NewFilmRequest(form["name"], file, directors, actors, genders);

            var map = mapper.W<NewFilmCommand>(newFilmRequest);
            var result = await sender.Send(map, cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        }).Accepts<NewFilmRequest>("multipart/form-data");

        return app;
    }
}
