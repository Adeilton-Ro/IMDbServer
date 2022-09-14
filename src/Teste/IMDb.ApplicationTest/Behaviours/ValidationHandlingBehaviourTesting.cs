using FluentResults;
using FluentValidation;
using IMDb.Application.Behaviours;
using MediatR;
using Moq;
using Xunit;

namespace IMDb.ApplicationTest.Behaviours;
public class Person : IRequest<Result>
{
    public string Name { get; set; }
}

public class PersonValidation : AbstractValidator<Person>
{
    public PersonValidation()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}
public class ValidationHandlingBehaviourTesting
{
    private readonly ValidationHandlingBehaviour<Person, Result> Validator = 
        new ValidationHandlingBehaviour<Person, Result>(new List<IValidator<Person>> { new PersonValidation() });
    private readonly Mock<RequestHandlerDelegate<Result>> nextMock = new();
    public ValidationHandlingBehaviourTesting()
    {
        nextMock.Setup(n => n()).ReturnsAsync(Result.Ok());
    }
    [Fact]
    public async Task Pass_With_Success()
    {
        var request = new Person { Name = "PassName" };
        var result = await Validator.Handle(request, CancellationToken.None, nextMock.Object);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Name_Was_Empty()
    {
        var request = new Person { Name = "" };
        var result = await Validator.Handle(request, CancellationToken.None, nextMock.Object);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
    }
}