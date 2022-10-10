using FluentResults;
using FluentValidation;
using IMDb.Application.Behaviours;
using MediatR;
using Moq;
using Xunit;

namespace IMDb.ApplicationTest.Behaviours;
public class TestRequest : IRequest<Result>
{
    public string Name { get; set; } = string.Empty;
}

public class PersonValidation : AbstractValidator<TestRequest>
{
    public PersonValidation()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}
public class ValidationHandlingBehaviourTesting
{
    private readonly ValidationHandlingBehaviour<TestRequest, Result> Validator = 
        new ValidationHandlingBehaviour<TestRequest, Result>(new List<IValidator<TestRequest>> { new PersonValidation() });
    private readonly Mock<RequestHandlerDelegate<Result>> nextMock = new();
    public ValidationHandlingBehaviourTesting()
    {
        nextMock.Setup(n => n()).ReturnsAsync(Result.Ok());
    }
    [Fact]
    public async Task Pass_With_Success()
    {
        var request = new TestRequest { Name = "PassName" };
        var result = await Validator.Handle(request, CancellationToken.None, nextMock.Object);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Name_Was_Empty()
    {
        var request = new TestRequest { Name = "" };
        var result = await Validator.Handle(request, CancellationToken.None, nextMock.Object);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
    }
}