using FluentResults;
using IMDb.Application.Commun;
using IMDb.Application.Services.UserInfo;
using MediatR;
using System.Reflection;

namespace IMDb.Application.Behaviours;
public class SetUserInfoBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ResultBase, new()
    where TRequest : IRequest<TResponse>
{
    private readonly IUserInfoService userInfoService;

    public SetUserInfoBehaviour(IUserInfoService userInfoService)
    {
        this.userInfoService = userInfoService;
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestType = typeof(TRequest);
        var properties = requestType.GetProperties();
        var UserInfos = properties.Where(p => p.GetCustomAttribute<FromUserInfoAttribute>() is not null);
        foreach(var userInfo in UserInfos)
            userInfo.SetValue(request, userInfoService.Id);

        return await next();
    }
}