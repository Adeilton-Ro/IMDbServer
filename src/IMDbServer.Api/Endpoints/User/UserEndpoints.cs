using AutoMapper;
using IMDb.Application.Features.Account.Clients.Disable;
using IMDb.Application.Features.Account.Clients.Edit;
using IMDb.Application.Features.Account.Clients.SignUp;
using IMDb.Application.Features.Auth.Clients.Login;
using IMDbServer.Api.Endpoints.User.CustomizedRequest.Disable;
using IMDbServer.Api.Endpoints.User.CustomizedRequest.Edit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDbServer.Api.Endpoints.User;
public static class UserEndpoints
{
    public static WebApplication UseUserEndpoints(this WebApplication app)
    {
        app.MapPost("user/signup",
            async ([FromServices] ISender sender, [FromBody] SignUpCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return MapAllEndpoints.SendResponse(result);
            });

        app.MapPost("user/login",
        async ([FromServices] ISender sender, [FromBody] LoginCommand request, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });

        app.MapPut("user/edit",
            [Authorize(Roles = "Client")] async ([FromServices] ISender sender, [FromServices] IMapper mapper,[FromBody] EditClientRequest request, CancellationToken cancellationToken) =>
            {
                var map = mapper.Map<EditClientCommand>(request);
                var result = await sender.Send(map, cancellationToken);
                return MapAllEndpoints.SendResponse(result);
            });

        app.MapPut("user/disable", [Authorize(Roles = "Client")] async ([FromServices] ISender sender, [FromServices] IMapper mapper, 
            [FromBody] DisableClientAccountRequest request, CancellationToken cancellationToken) =>
        {
            var map = mapper.Map<DisableClientAccountCommand>(request);
            var result = await sender.Send(map, cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });

        return app;
    }
}