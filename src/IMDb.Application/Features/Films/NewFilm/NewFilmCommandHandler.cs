using FluentResults;
using IMDb.Application.Extension;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using IMDb.Infra.FileSystem.Abstraction;
using IMDb.Infra.FileSystem.Abstraction.Interfaces.FileRepositories;
using MediatR;

namespace IMDb.Application.Features.Films.NewFilm;
public class NewFilmCommandHandler : IRequestHandler<NewFilmCommand, Result<NewFilmCommandResponse>>
{
    private readonly IFilmRepository filmRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IFileRepository fileRepository;

    public NewFilmCommandHandler(IFilmRepository filmRepository, IUnitOfWork unitOfWork, IFileRepository fileRepository)
    {
        this.filmRepository = filmRepository;
        this.unitOfWork = unitOfWork;
        this.fileRepository = fileRepository;
    }
    public async Task<Result<NewFilmCommandResponse>> Handle(NewFilmCommand request, CancellationToken cancellationToken)
    {
        if (!await filmRepository.NameAlredyExist(request.Name, cancellationToken))
            return Result.Fail(new ApplicationError("this film alredy exist"));

        var actorFilms = request.Actors.Select(a => new ActorFilm { Id = Guid.NewGuid(), ActorId = a});
        var directorFilms = request.Directors.Select(d => new DirectorFilm { Id = Guid.NewGuid(), DirectorId = d});
        var genderFilms = request.Genders.Select(g => new GenderFilm { Id = Guid.NewGuid(), GenderId = g});
         
        var namedFileImages = request.Images.Select(i => new NamedFileImage(Guid.NewGuid().ToString(), i));

        var paths = fileRepository.SaveFilmImage(namedFileImages, request.Name);
        var filmImages = paths.Select(p => new FilmImage { Id = Guid.NewGuid(), Uri = p });

        var film = new Film
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            ActorFilms = actorFilms,
            DirectorFilms = directorFilms,
            FilmImages = filmImages,
            GenderFilm = genderFilms
        };

        await filmRepository.Create(film, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new NewFilmCommandResponse(film.Id));
    }
}