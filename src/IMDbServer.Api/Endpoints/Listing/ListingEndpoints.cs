using IMDb.Application.Features.Listing.GetActiveClients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDbServer.Api.Endpoints.Listing;
public static class ListingEndpoints
{
    public static WebApplication UseListingEndpoints(this WebApplication app)
    {
        app.MapGet("listing/activeclients", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetActiveClientsQuery(), cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });

        return app;
    }
}