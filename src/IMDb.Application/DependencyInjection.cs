using FluentValidation;
using IMDb.Application.Behaviours;
using IMDb.Application.Services.Crypto;
using IMDb.Application.Services.Token;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IMDb.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationHandlingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehaviour<,>));

        services.AddValidatorsFromAssemblyContaining(typeof(SignUpCommandValidation));

        services.AddScoped<ICryptographyService, CryptographyService>();
        services.AddScoped<ITokenService, JwtTokenService>();

        return services;
    }
}