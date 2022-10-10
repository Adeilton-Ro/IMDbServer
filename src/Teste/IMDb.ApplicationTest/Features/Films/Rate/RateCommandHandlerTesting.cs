using FluentResults;
using IMDb.Application.Features.Films.Rate;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using Moq;
using Xunit;

namespace IMDb.ApplicationTest.Features.Films.Rate;
public class RateCommandHandlerTesting
{
	private readonly Mock<IUserRepository<Client>> userRepositoryMock = new();
	private readonly Mock<IFilmRepository> filmRepositoryMock = new();
	private readonly Mock<IUnitOfWork> unitOfWorkMock = new();
	private readonly Mock<IVoteRepository> voteRepositoryMock = new();

	private readonly List<Client> userList = new List<Client>
	{
		new Client
		{
			Id = Guid.Parse("f19a6c60-4f61-41e3-a5cd-1110a195c908")
		},
		new Client
		{
			Id = Guid.Parse("cd6a8e32-80f1-49c1-8c56-05850055bfca")
		}
	};

	private readonly List<Film> filmList = new List<Film>
	{
		new Film
		{
			Id = Guid.Parse("51e12a11-029c-4c0e-9f88-d05b2c4a5513"),
			Voters = 4,
			Average = 2
		}
	};

	private readonly List<Vote> context = new List<Vote>
	{
		new Vote
		{
			Id = Guid.Parse("8f469401-7612-4e00-87ff-04d91b8cc56f"),
			ClientId = Guid.Parse("cd6a8e32-80f1-49c1-8c56-05850055bfca"),
			FilmId = Guid.Parse("51e12a11-029c-4c0e-9f88-d05b2c4a5513")
        }
	};

	public RateCommandHandlerTesting()
	{
		userRepositoryMock.Setup(ur => ur.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync((Guid id, CancellationToken ct) => userList.FirstOrDefault(ul => ul.Id == id));

		filmRepositoryMock.Setup(fr => fr.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken ct) => filmList.FirstOrDefault(ul => ul.Id == id));
		filmRepositoryMock.Setup(fr => fr.Update(It.IsAny<Film>()))
			.Callback((Film film) => { filmList.Remove(filmList.First(f => f.Id == film.Id)); filmList.Add(film); });

		voteRepositoryMock.Setup(vr => vr.Create(It.IsAny<Vote>(), It.IsAny<CancellationToken>()))
			.Callback((Vote v, CancellationToken ct) => context.Add(v))
			.Returns((Vote v, CancellationToken ct) => Task.CompletedTask);

		voteRepositoryMock.Setup(vr => vr.IsAlredyRated(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync((Guid ci, Guid fi, CancellationToken cancellationToken) => context.Any(c => c.ClientId == ci && c.FilmId == fi));
    }

	[Fact]
	public async Task Success()
	{
		var request = new RateCommand(Guid.Parse("f19a6c60-4f61-41e3-a5cd-1110a195c908"), Guid.Parse("51e12a11-029c-4c0e-9f88-d05b2c4a5513"), 2);
		var handler = new RateCommandHandler(userRepositoryMock.Object, filmRepositoryMock.Object, voteRepositoryMock.Object, unitOfWorkMock.Object);
		var result = await handler.Handle(request, CancellationToken.None);

		unitOfWorkMock.VerifyAll();
		Assert.True(result.IsSuccess);
		Assert.Empty(result.Errors);
		Assert.NotNull(result.ValueOrDefault);
		Assert.Equal(2, result.Value.Avarage);
		Assert.Equal(5, result.Value.Voters);
		Assert.Equal(2, context.Count);
	}
    [Fact]
    public async Task Client_Alredy_Rated()
    {
        var request = new RateCommand(Guid.Parse("cd6a8e32-80f1-49c1-8c56-05850055bfca"), Guid.Parse("51e12a11-029c-4c0e-9f88-d05b2c4a5513"), 2);
        var handler = new RateCommandHandler(userRepositoryMock.Object, filmRepositoryMock.Object, voteRepositoryMock.Object, unitOfWorkMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.ValueOrDefault);
    }
    [Fact]
    public async Task Film_Doesnt_Exist()
    {
        var request = new RateCommand(Guid.Parse("f19a6c60-4f61-41e3-a5cd-1110a195c908"), Guid.Parse("b89aee8a-bb84-463e-a55e-9ba00994ca26"), 2);
        var handler = new RateCommandHandler(userRepositoryMock.Object, filmRepositoryMock.Object, voteRepositoryMock.Object, unitOfWorkMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
		Assert.Null(result.ValueOrDefault);
    }
	[Fact]
    public async Task Client_Doesnt_Exist()
    {
        var request = new RateCommand(Guid.Parse("b89aee8a-bb84-463e-a55e-9ba00994ca26"), Guid.Parse("51e12a11-029c-4c0e-9f88-d05b2c4a5513"), 2);
        var handler = new RateCommandHandler(userRepositoryMock.Object, filmRepositoryMock.Object, voteRepositoryMock.Object, unitOfWorkMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.ValueOrDefault);
    }
}