using FluentResults;
using IMDb.Application.Extension;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Films.NewFilm;
public class NewFilmCommandHandler : IRequestHandler<NewFilmCommand, Result<NewFilmCommandResponse>>
{
    private readonly IFilmRepository filmRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ICastRepository<Actor> actorRepository;
    private readonly ICastRepository<Director> directorRepository;
    private readonly IGenderRepository genderRepository;

    public NewFilmCommandHandler(IFilmRepository filmRepository, IUnitOfWork unitOfWork,
        ICastRepository<Actor> actorRepository, ICastRepository<Director> directorRepository, IGenderRepository genderRepository)
    {
        this.filmRepository = filmRepository;
        this.unitOfWork = unitOfWork;
        this.actorRepository = actorRepository;
        this.directorRepository = directorRepository;
        this.genderRepository = genderRepository;
    }
    public async Task<Result<NewFilmCommandResponse>> Handle(NewFilmCommand request, CancellationToken cancellationToken)
    {
        if (await filmRepository.NameAlredyExist(request.Name, cancellationToken))
            return Result.Fail(new ApplicationError("this film alredy exist"));

        if (!await genderRepository.AreAlredyCreated(request.Genders, cancellationToken))
            return Result.Fail(new ApplicationError("one of the genders does not exist"));

        if (!await actorRepository.AreAlredyCreated(request.Actors, cancellationToken))
            return Result.Fail(new ApplicationError("one of the actors does not exist"));

        if (!await directorRepository.AreAlredyCreated(request.Directors, cancellationToken))
            return Result.Fail(new ApplicationError("one of the directors does not exist"));

        var actorFilms = request.Actors.Select(a => new ActorFilm { Id = Guid.NewGuid(), ActorId = a }).ToList();
        var directorFilms = request.Directors.Select(d => new DirectorFilm { Id = Guid.NewGuid(), DirectorId = d }).ToList();
        var genderFilms = request.Genders.Select(g => new GenderFilm { Id = Guid.NewGuid(), GenderId = g }).ToList();

        var film = new Film
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Synopsis = request.Synopsis,
            Release = request.Release,
            ActorFilms = actorFilms,
            DirectorFilms = directorFilms,
            GenderFilm = genderFilms
        };

        await filmRepository.Create(film, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new NewFilmCommandResponse(film.Id));
    }
}