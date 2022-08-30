using IMDb.Application;
using IMDb.Infra.DataBase;

namespace IMDbServer.Api;

public static class DenpendencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddDataBase(configuration);

        return services;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app.MapEndpoints();

        return app;
    }
}