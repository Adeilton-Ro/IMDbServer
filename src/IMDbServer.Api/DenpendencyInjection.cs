using IMDb.Application;
using IMDb.Application.Services.Token;
using IMDb.Application.Services.UserInfo;
using IMDb.Infra.DataBase;
using IMDbServer.Api.RequestHandling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Text;

namespace IMDbServer.Api;
public static class DenpendencyInjection
{
    public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(
            c =>
            {
                c.AddSecurityDefinition(
                    "token",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer",
                        In = ParameterLocation.Header,
                        Name = HeaderNames.Authorization
                    }
                );
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "token"
                                },
                            },
                            Array.Empty<string>()
                        }
                    }
                );
            }
        );
        return services;
    }

    public static IServiceCollection AddJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JWT_DECRYPT"));

        services.AddOptions<JwtTokenServiceOption>()
            .Configure(
                (options) => options.Key = tokenKey
            );

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        services.AddAuthorization();

        return services;
    }
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddDataBase(configuration);
        services.AddJwtAuthorization(configuration);
        services.AddHttpContextAccessor();
        services.AddScoped<IUserInfoService, RequestUserInfoService>();
        services.AddCustomSwaggerGen();

        return services;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app.MapEndpoints();

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}