using IMDb.Application;
using IMDb.Application.Features.Listing.GetActiveClients;
using IMDb.Application.Features.Listing.GetFilms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDbServer.Api.Endpoints.Listing;
public static class ListingEndpoints
{
    public static WebApplication UseListingEndpoints(this WebApplication app)
    {
        app.MapGet("listing/activeclients/{page:int=0}/{quantityOfItems:int=0}/{isDescending:bool=false}", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender,
            [FromRoute]int page, [FromRoute]int quantityOfItems, [FromRoute]bool isDescending, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetActiveClientsQuery(page, quantityOfItems, isDescending), cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });

        app.MapGet("listing/films/{directors=}/{name=}/{genders=}/{actors=}/{page:int=0}/{quantityOfItems:int=0}/{isDescending:bool=false}", 
            [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, [FromRoute] int page, 
            [FromRoute] int quantityOfItems, [FromRoute] bool isDescending, [FromRoute] string directors, 
            [FromRoute] string name, [FromRoute] string genders, [FromRoute] string actors, CancellationToken cancellationToken) =>
        {
            var listDirectors = directors.Split(",").Select(d => Utils.TryParseNullSafe(d));
            var listGenders = genders.Split(",").Select(g => Utils.TryParseNullSafe(g));
            var listActors = actors.Split(",").Select(a => Utils.TryParseNullSafe(a));

            var result = await sender.Send(new GetFilmsQuery(isDescending, page, quantityOfItems, listDirectors, name, listGenders, listActors), cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });
        return app;
    }
}