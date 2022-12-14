using AutoMapper;
using IMDb.Application.Features.Films.GetActor;
using IMDb.Application.Features.Films.GetDirectors;
using IMDb.Application.Features.Films.GetGender;
using IMDb.Application.Features.Films.NewActor;
using IMDb.Application.Features.Films.NewDirector;
using IMDb.Application.Features.Films.NewFilm;
using IMDb.Application.Features.Films.NewFilmsImages;
using IMDb.Application.Features.Films.NewGender;
using IMDb.Application.Features.Films.Rate;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewActor;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewDirector;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewFilmsImage;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.Rate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading;

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

            var newDirectorRequest = new NewDirectorRequest(form["name"], form["description"],file[0]);

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

            var newActorRequest = new NewActorRequest(form["name"], form["description"], file[0]);

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
        async ([FromServices] ISender sender, [FromBody] NewFilmCommand request, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });

        app.MapPost("film/newfilmimages", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, [FromServices] IMapper mapper
            , HttpRequest request, CancellationToken cancellationToken) =>
        {
            var form = await request.ReadFormAsync(cancellationToken);
            var files = request.Form.Files;

            var newActorRequest = new NewFilmsImagesRequest(Guid.Parse(form["id"]), files);

            var map = mapper.Map<NewFilmsImagesCommand>(newActorRequest);
            var result = await sender.Send(map, cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        }).Accepts<NewFilmsImagesRequest>("multipart/form-data");

        app.MapPost("film/rate", [Authorize(Roles = "Client")] async ([FromServices] ISender sender, [FromServices] IMapper mapper
            , [FromBody] RateRequest request, CancellationToken cancellationToken) =>
        {
            var map = mapper.Map<RateCommand>(request);
            var result = await sender.Send(map, cancellationToken);

            return MapAllEndpoints.SendResponse(result);
        });

        return app;
    }
}
