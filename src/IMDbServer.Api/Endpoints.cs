using FluentResults;
using IMDb.Application.Extension;
using IMDb.Application.Features.SignUp;
using MediatR;
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