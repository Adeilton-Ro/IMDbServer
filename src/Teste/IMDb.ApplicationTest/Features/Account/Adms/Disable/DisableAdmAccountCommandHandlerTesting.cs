using IMDb.Application.Features.Account.Adms.Disable;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using Moq;
using Xunit;

namespace IMDb.ApplicationTest.Features.Account.Adms.Disable;
public class DisableAdmAccountCommandHandlerTesting
{
    private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
    private readonly Mock<IUserRepository<Adm>> userRepositoryMock = new Mock<IUserRepository<Adm>>();
    private readonly DisableAdmAccountCommandHandler handler;

    private readonly List<Adm> context = new List<Adm>
        {
            new Adm
            {
                Id = Guid.Parse("6ce5fedb-f35a-4be4-8c0f-75df82718094"),
                isActive = true
            },
            new Adm
            {
                Id = Guid.Parse("88ffeeb9-7bf2-4a87-aab2-16be402a578d"),
                isActive = false
            }
        };

    public DisableAdmAccountCommandHandlerTesting()
    {
        userRepositoryMock.Setup(ur => ur.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken ct) => context.FirstOrDefault(c => c.Id == id));

        userRepositoryMock.Setup(ur => ur.Edit(It.IsAny<Adm>()))
            .Callback<Adm>(u => { context.Remove(context.FirstOrDefault(c => c.Id == u.Id)); context.Add(u); });

        handler = new DisableAdmAccountCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Disable_With_Success()
    {
        var request = new DisableAdmAccountCommand(Guid.Parse("6ce5fedb-f35a-4be4-8c0f-75df82718094"));
        var result = await handler.Handle(request, CancellationToken.None);

        unitOfWorkMock.VerifyAll();
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.False(context.FirstOrDefault(c => c.Id == request.Id).isActive);
    }

    [Fact]
    public async Task User_Wasnt_Found()
    {
        var request = new DisableAdmAccountCommand(Guid.Parse("b5efadc4-3269-4f00-9c04-c81b74ce1f70"));
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task User_Is_Already_Disable()
    {
        var request = new DisableAdmAccountCommand(Guid.Parse("88ffeeb9-7bf2-4a87-aab2-16be402a578d"));
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.False(context.FirstOrDefault(c => c.Id == request.Id).isActive);
    }
}
