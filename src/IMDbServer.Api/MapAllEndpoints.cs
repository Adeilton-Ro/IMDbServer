using FluentResults;
using IMDb.Application.Extension;
using IMDbServer.Api.Endpoints.Adm;
using IMDbServer.Api.Endpoints.Listing;
using IMDbServer.Api.Endpoints.User;

namespace IMDbServer.Api;
public static class MapAllEndpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.UseUserEndpoints();
        app.UseAdmsEndpoints();
        app.UseListingEndpoints();
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