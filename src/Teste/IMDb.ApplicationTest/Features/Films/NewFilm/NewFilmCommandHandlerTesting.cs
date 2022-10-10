using IMDb.Application.Features.Films.NewFilm;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using Moq;
using Xunit;

namespace IMDb.ApplicationTest.Features.Films.NewFilm;
public class NewFilmCommandHandlerTesting
{
    private readonly Mock<IFilmRepository> filmRepositoryMock = new();
    private readonly Mock<IGenderRepository> genderRepositoryMock = new();
    private readonly Mock<ICastRepository<Actor>> actorRepositoryMock = new();
    private readonly Mock<ICastRepository<Director>> directorRepositoryMock = new();
    private readonly Mock<IUnitOfWork> unitOfWorkMock = new();

    private readonly List<Film> context = new List<Film>
    {
        new Film
        {
            Name = "Star Wars"
        }
    };

    private readonly List<Gender> genderList = new List<Gender>
    {
        new Gender
        {
            Id = Guid.Parse("216dfdc6-34ce-4f47-978b-71ab4a32a7d0")
        }
    };

    private readonly List<Actor> actorList = new List<Actor>
    {
        new Actor
        {
            Id = Guid.Parse("149b5e80-8c5f-4a5f-b339-4a71e6fedb03")
        }
    };
    private readonly List<Director> directorList = new List<Director>
    {
        new Director
        {
            Id = Guid.Parse("2b3a306f-61ff-439b-a0be-eef149b658aa")
        }
    };

    public NewFilmCommandHandlerTesting()
    {
        filmRepositoryMock.Setup(fr => fr.NameAlredyExist(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string name, CancellationToken ct) => context.Any(c => c.Name.ToLower() == name.ToLower()));

        filmRepositoryMock.Setup(fr => fr.Create(It.IsAny<Film>(), It.IsAny<CancellationToken>()))
            .Callback((Film film, CancellationToken ct) => context.Add(film));

        genderRepositoryMock.Setup(gr => gr.AreAlredyCreated(It.IsAny<IEnumerable<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((IEnumerable<Guid> ids, CancellationToken ct) => genderList.Any(g => ids.Contains(g.Id)));

        actorRepositoryMock.Setup(ar => ar.AreAlredyCreated(It.IsAny<IEnumerable<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((IEnumerable<Guid> ids, CancellationToken ct) => actorList.Any(a => ids.Contains(a.Id)));

        directorRepositoryMock.Setup(dr => dr.AreAlredyCreated(It.IsAny<IEnumerable<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((IEnumerable<Guid> ids, CancellationToken ct) => directorList.Any(d => ids.Contains(d.Id)));
    }

    [Fact]
    public async Task Success()
    {
        var request = new NewFilmCommand("Lord of the Rings", "",DateTime.UtcNow, new List<Guid> { Guid.Parse("2b3a306f-61ff-439b-a0be-eef149b658aa") },
            new List<Guid> { Guid.Parse("149b5e80-8c5f-4a5f-b339-4a71e6fedb03") }, new List<Guid> { Guid.Parse("216dfdc6-34ce-4f47-978b-71ab4a32a7d0") });
        var handler = new NewFilmCommandHandler(filmRepositoryMock.Object, unitOfWorkMock.Object, actorRepositoryMock.Object, 
            directorRepositoryMock.Object, genderRepositoryMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        unitOfWorkMock.VerifyAll();
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Value);
        Assert.Equal(2, context.Count);
    }

    [Fact]
    public async Task Director_Doesnt_Exist()
    {
        var request = new NewFilmCommand("Lord of the Rings", "", DateTime.UtcNow, new List<Guid> { Guid.Parse("8dcce9b9-2baf-4e02-a45f-89b3f51ba507") },
            new List<Guid> { Guid.Parse("149b5e80-8c5f-4a5f-b339-4a71e6fedb03") }, new List<Guid> { Guid.Parse("216dfdc6-34ce-4f47-978b-71ab4a32a7d0") });
        var handler = new NewFilmCommandHandler(filmRepositoryMock.Object, unitOfWorkMock.Object, actorRepositoryMock.Object,
            directorRepositoryMock.Object, genderRepositoryMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.ValueOrDefault);
        Assert.Single(context);
    }

    [Fact]
    public async Task Actor_Doesnt_Exist()
    {
        var request = new NewFilmCommand("Lord of the Rings", "", DateTime.UtcNow, new List<Guid> { Guid.Parse("2b3a306f-61ff-439b-a0be-eef149b658aa") },
            new List<Guid> { Guid.Parse("9c47433d-851a-4578-b7e4-705be7a0c2ad") }, new List<Guid> { Guid.Parse("216dfdc6-34ce-4f47-978b-71ab4a32a7d0") });
        var handler = new NewFilmCommandHandler(filmRepositoryMock.Object, unitOfWorkMock.Object, actorRepositoryMock.Object,
            directorRepositoryMock.Object, genderRepositoryMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.ValueOrDefault);
        Assert.Single(context);
    }

    [Fact]
    public async Task Gender_Doesnt_Exist()
    {
        var request = new NewFilmCommand("Lord of the Rings", "", DateTime.UtcNow, new List<Guid> { Guid.Parse("2b3a306f-61ff-439b-a0be-eef149b658aa") },
            new List<Guid> { Guid.Parse("149b5e80-8c5f-4a5f-b339-4a71e6fedb03") }, new List<Guid> { Guid.Parse("173c909b-7084-4912-98a3-69d592523af1") });
        var handler = new NewFilmCommandHandler(filmRepositoryMock.Object, unitOfWorkMock.Object, actorRepositoryMock.Object,
            directorRepositoryMock.Object, genderRepositoryMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.ValueOrDefault);
        Assert.Single(context);
    }

    [Theory]
    [InlineData("Star Wars")]
    [InlineData("star wars")]
    public async Task The_Film_Alredy_Exist(string name)
    {
        var request = new NewFilmCommand(name, "", DateTime.UtcNow, new List<Guid> { Guid.Parse("2b3a306f-61ff-439b-a0be-eef149b658aa") },
            new List<Guid> { Guid.Parse("149b5e80-8c5f-4a5f-b339-4a71e6fedb03") }, new List<Guid> { Guid.Parse("216dfdc6-34ce-4f47-978b-71ab4a32a7d0") });
        var handler = new NewFilmCommandHandler(filmRepositoryMock.Object, unitOfWorkMock.Object, actorRepositoryMock.Object,
            directorRepositoryMock.Object, genderRepositoryMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.ValueOrDefault);
        Assert.Single(context);
    }
}