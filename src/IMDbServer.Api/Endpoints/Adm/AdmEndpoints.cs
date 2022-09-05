using IMDb.Application.Features.Account.Adms.Disable;
using IMDb.Application.Features.Account.Adms.Edit;
using IMDb.Application.Features.Account.Adms.SignUp;
using IMDb.Application.Features.Auth.Adms.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDbServer.Api.Endpoints.Adm;
public static class AdmEndpoints
{
    public static WebApplication UseAdmsEndpoints(this WebApplication app)
    {
        app.MapPost("adm/login",
            async ([FromServices] ISender sender, [FromBody] AdmLoginCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return MapAllEndpoints.SendResponse(result);
            });

        app.MapPost("adm/signup",
            [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, [FromBody] AdmSignUpCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return MapAllEndpoints.SendResponse(result);
            });

        app.MapPut("adm/edit",
            [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, [FromBody] EditAdmCommand request, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return MapAllEndpoints.SendResponse(result);
            });

        app.MapPut("adm/disable", [Authorize(Roles = "Adm")] async ([FromServices] ISender sender, [FromBody] DisableAdmAccountCommand request, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);
            return MapAllEndpoints.SendResponse(result);
        });

        return app;
    }
}