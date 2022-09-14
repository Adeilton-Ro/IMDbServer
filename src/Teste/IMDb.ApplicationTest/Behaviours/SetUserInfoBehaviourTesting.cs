using FluentResults;
using IMDb.Application.Behaviours;
using IMDb.Application.Commun;
using IMDb.Application.Services.UserInfo;
using MediatR;
using Moq;
using Xunit;

namespace IMDb.ApplicationTest.Behaviours;
public class InfoUserConcrete : IUserInfoService
{
    public Guid? Id => Guid.Parse("1612ae02-35d7-4a7b-9a3c-41ea9495343d");
}
public class UserWithUserInfo : IRequest<Result>
{
	[FromUserInfo]
	public Guid Id { get; set; }
}
public class UserWithoutUserInfo : IRequest<Result>
{
    public Guid Id { get; set; }
}
public class SetUserInfoBehaviourTesting
{
    private readonly Mock<RequestHandlerDelegate<Result>> nextMock = new();
    public SetUserInfoBehaviourTesting()
	{
        nextMock.Setup(n => n()).ReturnsAsync(Result.Ok());
    }

    [Fact]
    public async Task Add_Info()
    {
        var user = new UserWithUserInfo();
        var userInfoService = new InfoUserConcrete();
        var handler = new SetUserInfoBehaviour<UserWithUserInfo, Result>(userInfoService);
        var result = await handler.Handle(user, CancellationToken.None, nextMock.Object);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.Equal(user.Id, userInfoService.Id);
    }

    [Fact]
    public async Task Not_Add_Info()
    {
        var user = new UserWithoutUserInfo();
        var userInfoService = new InfoUserConcrete();
        var handler = new SetUserInfoBehaviour<UserWithoutUserInfo, Result>(userInfoService);
        var result = await handler.Handle(user, CancellationToken.None, nextMock.Object);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.Equal(user.Id, Guid.Empty);
    }
}