using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using IMDb.Infra.Database.Abstraction.Interfaces;
using Moq;
using IMDb.Application.Features.Account.Clients.Disable;
using Xunit;

namespace IMDb.ApplicationTest.Features.Account.Clients.Disable;
public class DisableClientAccountCommandHandlerTesting
{
    private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
    private readonly Mock<IUserRepository<Client>> userRepositoryMock = new Mock<IUserRepository<Client>>();
    private readonly DisableClientAccountCommandHandler handler;

    private readonly List<Client> context = new List<Client>
    {
        new Client
        {
            Id = Guid.Parse("6ce5fedb-f35a-4be4-8c0f-75df82718094"),
            isActive = true
        },
        new Client
        {
            Id = Guid.Parse("88ffeeb9-7bf2-4a87-aab2-16be402a578d"),
            isActive = false
        }
    };

    public DisableClientAccountCommandHandlerTesting()
    {
        userRepositoryMock.Setup(ur => ur.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken ct) => context.FirstOrDefault(c => c.Id == id));

        userRepositoryMock.Setup(ur => ur.Edit(It.IsAny<Client>()))
            .Callback<Client>(u => { context.Remove(context.FirstOrDefault(c => c.Id == u.Id)); context.Add(u); });

        handler = new DisableClientAccountCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Disable_With_Success()
    {
        var request = new DisableClientAccountCommand()
        {
            Id = Guid.Parse("6ce5fedb-f35a-4be4-8c0f-75df82718094")
        };
        var result = await handler.Handle(request, CancellationToken.None);

        unitOfWorkMock.VerifyAll();
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.False(context.FirstOrDefault(c => c.Id == request.Id).isActive);
    }

    [Fact]
    public async Task User_Wasnt_Found()
    {
        var request = new DisableClientAccountCommand()
        {
            Id = Guid.Parse("b5efadc4-3269-4f00-9c04-c81b74ce1f70")
        };
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task User_Is_Already_Disable()
    {
        var request = new DisableClientAccountCommand()
        {
            Id = Guid.Parse("88ffeeb9-7bf2-4a87-aab2-16be402a578d")
        };
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.False(context.FirstOrDefault(c => c.Id == request.Id).isActive);
    }
}