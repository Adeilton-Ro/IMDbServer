using IMDb.Application.Services.Crypto;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using IMDb.Infra.Database.Abstraction.Interfaces;
using Moq;
using Xunit;
using IMDb.Application.Features.Account.Adms.Edit;

namespace IMDb.ApplicationTest.Features.Account.Adms.Edit;
public class EditAdmCommandHandlerTesting
{
    public static (Mock<IUserRepository<Adm>> userRepositoryMock, Mock<IUnitOfWork> unitOfWorkMock, List<Adm> context) GetDependency()
    {
        var cryptographyService = new CryptographyService();
        var salt = cryptographyService.CreateSalt();

        var context = new List<Adm>
        {
            new Adm
            {
                Id = Guid.Parse("6ce5fedb-f35a-4be4-8c0f-75df82718094"),
                Name = "Test",
                Email = "teste@imdbserver.com",
                Hash = cryptographyService.Hash("Password", salt),
                Salt = salt
            },
            new Adm
            {
                Id = Guid.Parse("9e90867e-83c3-41d2-aaf6-d1791fdaac53"),
                Name = "Test",
                Email = "sameemail@imdbserver.com",
                Hash = cryptographyService.Hash("Password", salt),
                Salt = salt
            }
        };
        var userRepositoryMock = new Mock<IUserRepository<Adm>>();
        userRepositoryMock.Setup(ur => ur.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken ct) => context.FirstOrDefault(c => c.Id == id));
        userRepositoryMock.Setup(ur => ur.Edit(It.IsAny<Adm>()))
            .Callback<Adm>(user => { context.Remove(context.FirstOrDefault(c => c.Id == user.Id)); context.Add(user); });
        userRepositoryMock.Setup(ur => ur.IsUniqueEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string email, CancellationToken ct) => !context.Any(c => c.Email == email));

        return (userRepositoryMock, new Mock<IUnitOfWork>(), context);
    }

    [Theory]
    [InlineData("teste@imdbserver.com")]
    [InlineData("otheremail@otheremail.com")]
    public async Task Edit_With_Success(string email)
    {
        (Mock<IUserRepository<Adm>> userRepositoryMock, Mock<IUnitOfWork> unitOfWorkMock, List<Adm> context) = GetDependency();

        var request = new EditAdmCommand("OtherName", email)
        {
            Id = Guid.Parse("6ce5fedb-f35a-4be4-8c0f-75df82718094")
        };
        var handler = new EditAdmCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        unitOfWorkMock.VerifyAll();
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.Equal(context.FirstOrDefault(c => c.Id == request.Id).Email, request.Email);
        Assert.Equal(context.FirstOrDefault(c => c.Id == request.Id).Name, request.Name);
    }

    [Fact]
    public async Task Client_Wasnt_Found()
    {
        (Mock<IUserRepository<Adm>> userRepositoryMock, Mock<IUnitOfWork> unitOfWorkMock, List<Adm> context) = GetDependency();

        var request = new EditAdmCommand("OtherName", "otheremail@otheremail.com")
        {
            Id = Guid.Parse("40128e11-6099-4551-9ba9-9f12605f7ff9")
        };
        var handler = new EditAdmCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task Email_Was_Different_And_Not_Unique()
    {
        (Mock<IUserRepository<Adm>> userRepositoryMock, Mock<IUnitOfWork> unitOfWorkMock, List<Adm> context) = GetDependency();

        var request = new EditAdmCommand("OtherName", "sameemail@imdbserver.com")
        {
            Id = Guid.Parse("6ce5fedb-f35a-4be4-8c0f-75df82718094")
        };
        var handler = new EditAdmCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
    }
}