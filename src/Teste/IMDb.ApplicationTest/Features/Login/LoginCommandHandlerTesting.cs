using IMDb.Application.Features.Login;
using IMDb.Application.Services.Crypto;
using IMDb.Application.Services.Token;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using Moq;
using Xunit;

namespace IMDb.ApplicationTest.Features.Login;
public class LoginCommandHandlerTesting
{
    public static (Mock<IUserRepository<Client>> UserRepositoryMock, ICryptographyService cryptographyService, Mock<ITokenService> tokenServiceMock) GetDependency()
    {
        var cryptographyService = new CryptographyService();
        var salt = cryptographyService.CreateSalt();

        var context = new List<Client>
        {
            new Client
            {
                Email = "teste@imdbserver.com",
                Hash = cryptographyService.Hash("Password", salt),
                Salt = salt,
            }
        };

        var tokenServiceMock = new Mock<ITokenService>();
        tokenServiceMock.Setup(ts => ts.GenerateToken(It.IsAny<Client>())).Returns((Client client) => $"The Client Email is: {client.Email}");

        var userRepositoryMock = new Mock<IUserRepository<Client>>();
        userRepositoryMock.Setup(ur => ur.GetByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string email, CancellationToken cancellationToken) => context.FirstOrDefault(c => c.Email == email));
        return (userRepositoryMock, cryptographyService, tokenServiceMock);
    }

    [Fact]
    public async Task Is_Success_Login()
    {
        (Mock<IUserRepository<Client>> UserRepositoryMock, ICryptographyService cryptographyService,
        Mock<ITokenService> tokenServiceMock) = GetDependency();

        var request = new LoginCommand("teste@IMDbServer.com", "Password");
        var handler = new LoginCommandHandler(UserRepositoryMock.Object, cryptographyService, tokenServiceMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.ValueOrDefault);
        Assert.Equal(result.Value.Token, $"The Client Email is: {request.Email.ToLower()}");
    }

    [Theory]
    [InlineData("12345678")]
    [InlineData("password")]
    public async Task Password_Was_Wrong(string password)
    {
        (Mock<IUserRepository<Client>> UserRepositoryMock, ICryptographyService cryptographyService,
        Mock<ITokenService> tokenServiceMock) = GetDependency();

        var request = new LoginCommand("teste@IMDbServer.com", password);
        var handler = new LoginCommandHandler(UserRepositoryMock.Object, cryptographyService, tokenServiceMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.ValueOrDefault);
    }

    [Fact]
    public async Task Email_Was_Wrong()
    {
        (Mock<IUserRepository<Client>> UserRepositoryMock, ICryptographyService cryptographyService,
        Mock<ITokenService> tokenServiceMock) = GetDependency();

        var request = new LoginCommand("WrongEmail@IMDbServer.com", "Password");
        var handler = new LoginCommandHandler(UserRepositoryMock.Object, cryptographyService, tokenServiceMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.ValueOrDefault);
    }
}