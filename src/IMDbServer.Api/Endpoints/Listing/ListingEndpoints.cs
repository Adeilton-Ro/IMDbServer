using IMDb.Application.Features.Listing.GetActiveClients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDbServer.Api.Endpoints.Listing;
public static class ListingEndpoints
{
    public static WebApplication UseListingEndpoints(this WebApplication app)
    {
        app.MapGet("listing/activeclients/{page:int}/{quantityOfItems:int}/{isDescending:bool=false}", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender,
            [FromRoute]int page, [FromRoute]int quantityOfItems, [FromRoute]bool isDescending, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetActiveClientsQuery(page, quantityOfItems, isDescending), cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });

        return app;
    }
}