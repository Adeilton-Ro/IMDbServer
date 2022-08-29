using FluentResults;
using IMDb.Application.Extension;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IMDb.Application.Behaviours;

public class ErrorHandlingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ResultBase, new()
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ErrorHandlingBehaviour<TRequest, TResponse>> logger;

    public ErrorHandlingBehaviour(ILogger<ErrorHandlingBehaviour<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "UnKnow Error");
            return Result.Fail(new Error("Unknow Error")).To<TResponse>();
        }
    }
}