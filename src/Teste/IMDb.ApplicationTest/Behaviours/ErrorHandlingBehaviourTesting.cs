using FluentResults;
using IMDb.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace IMDb.ApplicationTest.Behaviours;

public class ErrorHandlingBehaviourTesting
{
    [Fact]
    public async Task Return_Error_AnyException()
    {
        var loggerMock = new Mock<ILogger<ErrorHandlingBehaviour<TestRequest, Result>>>();

        var handler = new ErrorHandlingBehaviour<TestRequest, Result>(loggerMock.Object);

        var nextMock = new Mock<RequestHandlerDelegate<Result>>();
        nextMock.Setup(n => n()).ThrowsAsync(new Exception());

        var result = await handler.Handle(new TestRequest(), CancellationToken.None, nextMock.Object);

        loggerMock.VerifyAll();
        nextMock.VerifyAll();
        Assert.True(result.IsFailed);
    }

    [Fact]
    public async Task Doesnt_Return_Error()
    {
        var loggerMock = new Mock<ILogger<ErrorHandlingBehaviour<TestRequest, Result>>>();

        var handler = new ErrorHandlingBehaviour<TestRequest, Result>(loggerMock.Object);

        var nextMock = new Mock<RequestHandlerDelegate<Result>>();
        nextMock.Setup(n => n()).ReturnsAsync(Result.Ok());

        var result = await handler.Handle(new TestRequest(), CancellationToken.None, nextMock.Object);

        Assert.True(result.IsSuccess);
    }
}
