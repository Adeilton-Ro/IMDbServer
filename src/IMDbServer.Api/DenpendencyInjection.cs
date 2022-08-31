using IMDb.Application;
using IMDb.Application.Services.Token;
using IMDb.Infra.DataBase;
using System.Text;

namespace IMDbServer.Api;

public static class DenpendencyInjection
{
    public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
    {
        services.AddOptions<JwtTokenServiceOption>()
            .Configure<IConfiguration>(
                (options, configuration) => options.Key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JWT_DECRYPT"))
            );

        return services;
    }
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddDataBase(configuration);
        services.AddJwtAuthorization();

        return services;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app.MapEndpoints();

        return app;
    }
}