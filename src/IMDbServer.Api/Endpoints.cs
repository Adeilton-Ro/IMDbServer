using FluentResults;
using IMDb.Application.Extension;
using IMDb.Application.Features.Account.Adms.Disable;
using IMDb.Application.Features.Account.Adms.Edit;
using IMDb.Application.Features.Account.Adms.SignUp;
using IMDb.Application.Features.Account.Clients.Disable;
using IMDb.Application.Features.Account.Clients.Edit;
using IMDb.Application.Features.Account.Clients.SignUp;
using IMDb.Application.Features.Auth.Adms.Login;
using IMDb.Application.Features.Auth.Clients.Login;
using IMDb.Application.Features.Listing.GetActiveClients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        app.MapPut("/edit",
            [Authorize(Roles = "Client")] async ([FromServices] ISender sender, [FromBody] EditClientCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return SendResponse(result);
            });

        app.MapPut("/disable", [Authorize(Roles = "Client")] async ([FromServices] ISender sender, [FromBody] DisableClientAccountCommand request, CancellationToken cancellationToken) =>
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

        app.MapPut("adm/edit",
            [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, [FromBody] EditAdmCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return SendResponse(result);
            });

        app.MapPut("adm/disable", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, [FromBody] DisableAdmAccountCommand request, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);
            return SendResponse(result);
        });

        app.MapGet("listing/activeclients", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetActiveClientsQuery(), cancellationToken);
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