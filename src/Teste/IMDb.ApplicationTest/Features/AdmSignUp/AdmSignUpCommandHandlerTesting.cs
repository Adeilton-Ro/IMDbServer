using IMDb.Application.Services.Crypto;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using IMDb.Infra.Database.Abstraction.Interfaces;
using Moq;
using Xunit;
using IMDb.Application.Features.AdmSignUp;

namespace IMDb.ApplicationTest.Features.AdmSignUp;
public class AdmSignUpCommandHandlerTesting
{
    public static (Mock<IUserRepository<Adm>> userRepositoryMock, Mock<ICryptographyService> cryptoServiceMock,
        Mock<IUnitOfWork> unitOfWorkMock, List<Adm> context) GetDependency()
    {
        var context = new List<Adm>
        {
            new Adm
            {
                Email = "teste@imdbserver.com"
            }
        };

        var userRepository = new Mock<IUserRepository<Adm>>();
        userRepository.Setup(ur => ur.Create(It.IsAny<Adm>(), It.IsAny<CancellationToken>()))
            .Callback((Adm user, CancellationToken cancellationToken) => context.Add(user))
            .Returns((Adm user, CancellationToken cancellationToken) => Task.CompletedTask);

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
        (Mock<IUserRepository<Adm>> userRepositoryMock, Mock<ICryptographyService> cryptoServiceMock,
        Mock<IUnitOfWork> unitOfWorkMock, List<Adm> context) = GetDependency();

        var request = new AdmSignUpCommand("User", "CorrectEmail@IMDbServer.com", "NewPassword");
        var handler = new AdmSignUpCommandHandler(userRepositoryMock.Object, cryptoServiceMock.Object, unitOfWorkMock.Object);
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
        (Mock<IUserRepository<Adm>> userRepositoryMock, Mock<ICryptographyService> cryptoServiceMock,
        Mock<IUnitOfWork> unitOfWorkMock, List<Adm> context) = GetDependency();

        var request = new AdmSignUpCommand("User", "Teste@IMDbServer.com", "NewPassword");
        var handler = new AdmSignUpCommandHandler(userRepositoryMock.Object, cryptoServiceMock.Object, unitOfWorkMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.ValueOrDefault);
        Assert.Single(context);
    }
}