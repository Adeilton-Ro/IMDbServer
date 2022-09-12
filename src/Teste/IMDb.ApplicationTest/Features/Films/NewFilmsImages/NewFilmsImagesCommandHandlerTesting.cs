using IMDb.Application.Features.Films.NewFilmsImages;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using IMDb.Infra.FileSystem.Abstraction;
using IMDb.Infra.FileSystem.Abstraction.Interfaces.FileRepositories;
using Moq;
using Xunit;

namespace IMDb.ApplicationTest.Features.Films.NewFilmsImages;
public class NewFilmsImagesCommandHandlerTesting
{
    private readonly Mock<IFileRepository> fileRepositoryMock = new();
    private readonly Mock<IFilmRepository> filmRepositoryMock = new();
    private readonly Mock<IUnitOfWork> unitOfWorkMock = new();

    private readonly List<FilmImage> context = new();
    private readonly List<Film> filmList = new List<Film>
    {
        new Film
        {
            Id = Guid.Parse("c2e6c39e-32aa-4e67-bf84-8e507e065fa8"),
            Name = "Star Wars"
        }
    };

    public NewFilmsImagesCommandHandlerTesting()
    {
        fileRepositoryMock.Setup(fr => fr.SaveFilmImage(It.IsAny<IEnumerable<NamedFileImage>>(), It.IsAny<string>()))
            .Returns((IEnumerable<NamedFileImage> images, string filmName) => new List<string> { "returning a movie image path" });

        filmRepositoryMock.Setup(fr => fr.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken ct) => filmList.FirstOrDefault(f => f.Id == id));

        filmRepositoryMock.Setup(fr => fr.NewImages(It.IsAny<IEnumerable<FilmImage>>(), It.IsAny<CancellationToken>()))
            .Callback((IEnumerable<FilmImage> images, CancellationToken ct) => context.AddRange(images));
    }

    [Fact]
    public async Task Success()
    {
        var request = new NewFilmsImagesCommand(Guid.Parse("c2e6c39e-32aa-4e67-bf84-8e507e065fa8"), new List<FileImage> { new FileImage(".jpg", null) });
        var handler = new NewFilmsImagesCommandHandler(filmRepositoryMock.Object, fileRepositoryMock.Object, unitOfWorkMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        unitOfWorkMock.VerifyAll();
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.Equal(result.Value.Paths.First(), "returning a movie image path");
        Assert.NotEmpty(context);
    }

    [Fact]
    public async Task The_Film_Doesnt_Exist()
    {
        var request = new NewFilmsImagesCommand(Guid.Parse("0a9488f0-07d1-4729-898c-738aeeb01260"), new List<FileImage> { new FileImage(".jpg", null) });
        var handler = new NewFilmsImagesCommandHandler(filmRepositoryMock.Object, fileRepositoryMock.Object, unitOfWorkMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.ValueOrDefault);
        Assert.Empty(context);
    }
}