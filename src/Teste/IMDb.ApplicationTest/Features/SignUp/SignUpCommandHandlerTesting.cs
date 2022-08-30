using IMDb.Application.Services.Crypto;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using IMDb.Infra.Database.Abstraction.Interfaces;
using Moq;
using Xunit;
using IMDb.Application.Features.SignUp;
using IMDb.Domain.Entities.Abstract;
using IMDb.Domain.Entities;

namespace IMDb.ApplicationTest.Features.SignUp;
public class SignUpCommandHandlerTesting
{
    public static (Mock<IUserRepository<Client>> userRepositoryMock, Mock<ICryptographyService> cryptoServiceMock,
        Mock<IUnitOfWork> unitOfWorkMock, List<Client> context) GetDependency()
    {
        var context = new List<Client>
        {
            new Client
            {
                Email = "Teste@IMDbServer.com"
            }
        };

        var userRepository = new Mock<IUserRepository<Client>>();
        userRepository.Setup(ur => ur.Create(It.IsAny<Client>(), It.IsAny<CancellationToken>()))
            .Callback((Client user, CancellationToken cancellationToken) => context.Add(user))
            .Returns((Client user, CancellationToken cancellationToken) => Task.CompletedTask);

        userRepository.Setup(ur => ur.IsUniqueEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns((string email, CancellationToken cancellationToken) => Task.FromResult(context.Any(c => c.Email == email)));

        var cryptographyService = new Mock<ICryptographyService>();
        cryptographyService.Setup(cs => cs.CreateSalt()).Returns(() => "Random Salt");

        cryptographyService.Setup(cs => cs.Hash(It.IsAny<string>(), It.IsAny<string>()))
            .Returns((string plainText, string Salt) => $"Password: {plainText} and Salt: {Salt}");

        return (userRepository, cryptographyService, new Mock<IUnitOfWork>(), context);
    }

    [Fact]
    public async Task Success_Sign_Up()
    {
        (Mock<IUserRepository<Client>> userRepositoryMock, Mock<ICryptographyService> cryptoServiceMock,
        Mock<IUnitOfWork> unitOfWorkMock, List<Client> context) = GetDependency();

        var request = new SignUpCommand("CorrectEmail@IMDbServer.com", "User", "NewPassword");
        var handler = new SignUpCommandHandler(userRepositoryMock.Object, cryptoServiceMock.Object, unitOfWorkMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        cryptoServiceMock.VerifyAll();
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()));
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Value);
        Assert.Equal(context.Count, 2);
    }

    [Fact]
    public async Task Email_Is_Already_In_Use()
    {
        (Mock<IUserRepository<Client>> userRepositoryMock, Mock<ICryptographyService> cryptoServiceMock,
        Mock<IUnitOfWork> unitOfWorkMock, List<Client> context) = GetDependency();

        var request = new SignUpCommand("Teste@IMDbServer.com", "User", "NewPassword");
        var handler = new SignUpCommandHandler(userRepositoryMock.Object, cryptoServiceMock.Object, unitOfWorkMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.ValueOrDefault);
        Assert.Single(context);
    }
}