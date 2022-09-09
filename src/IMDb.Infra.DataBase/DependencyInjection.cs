using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using IMDb.Infra.DataBase.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IMDb.Infra.DataBase;
public static class DependencyInjection
{
    public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IMDbServerDbContext>(options => options.UseSqlServer(configuration.GetValue<string>("IMDbServer_Connection")));
        services.AddScoped(sp => sp.GetRequiredService<IMDbServerDbContext>() as IUnitOfWork);
        services.AddRepositories();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository<Client>, UserRepository<Client>>();
        services.AddScoped<IUserRepository<Adm>, UserRepository<Adm>>();
        services.AddScoped<ICastRepository<Director>, CastRepository<Director>>();
        services.AddScoped<IGenderRepository, GenderRepository>();
        services.AddScoped<ICastRepository<Actor>, CastRepository<Actor>>();

        return services;
    }
}