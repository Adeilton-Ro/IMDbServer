using IMDb.Infra.FileSystem.FileRepositories;
using IMDb.Infra.FileSystem.Abstraction.Interfaces.FileRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace IMDb.Infra.FileSystem;
public static class DependencyInjection
{
    public static IServiceCollection AddFileSystem(this IServiceCollection services)
    {
        services.AddScoped<IFileRepository, FileRepository>();
        return services;
    }
}