using FluentResults;
using IMDb.Application.Extension;
using IMDb.Application.Features.AdmEdit;
using IMDb.Application.Features.AdmLogin;
using IMDb.Application.Features.AdmSignUp;
using IMDb.Application.Features.ClientEdit;
using IMDb.Application.Features.ClientLogin;
using IMDb.Application.Features.ClientSignUp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IMDbServer.Api;
public static class Endpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapPost("/signup",
            async ([FromServices] ISender sender, [FromBody] SignUpCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return SendResponse(result);
            });

        app.MapPost("/login",
            async ([FromServices] ISender sender, [FromBody] LoginCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return SendResponse(result);
            });

        app.MapPost("/edit",
            [Authorize(Roles = "Client")] async ([FromServices] ISender sender, [FromBody] EditClientCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return SendResponse(result);
            });

        app.MapPost("adm/login",
            async ([FromServices] ISender sender, [FromBody] AdmLoginCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return SendResponse(result);
            });

        app.MapPost("adm/signup",
            [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, [FromBody] AdmSignUpCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return SendResponse(result);
            });

        app.MapPost("adm/edit",
            [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, [FromBody] EditAdmCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return SendResponse(result);
            });

        return app;
    }

    public static IResult SendResponse<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return SendResponse(result.ToResult());
    }

    public static IResult SendResponse(Result result)
    {
        if (result.IsSuccess)
            return Results.Ok();

        if (result.Errors.First() is ValidationError validationError)
            return Results.BadRequest(validationError.ErrorMessageDictionary);

        if (result.Errors.First() is ApplicationError application)
            return Results.Json(new { application.Message }, statusCode: 403);

        return Results.Json(new { result.Errors.First().Message }, statusCode: 500);
    }
}