using FluentResults;
using IMDb.Application.Extension;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using IMDb.Infra.FileSystem.Abstraction;
using IMDb.Infra.FileSystem.Abstraction.Interfaces.FileRepositories;
using MediatR;

namespace IMDb.Application.Features.Films.NewFilmsImages;
public class NewFilmsImagesCommandHandler : IRequestHandler<NewFilmsImagesCommand, Result<NewFilmsImagesCommandResponse>>
{
    private readonly IFilmRepository filmRepository;
    private readonly IFileRepository fileRepository;
    private readonly IUnitOfWork unitOfWork;

    public NewFilmsImagesCommandHandler(IFilmRepository filmRepository, IFileRepository fileRepository, IUnitOfWork unitOfWork)
    {
        this.filmRepository = filmRepository;
        this.fileRepository = fileRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Result<NewFilmsImagesCommandResponse>> Handle(NewFilmsImagesCommand request, CancellationToken cancellationToken)
    {
        var film = await filmRepository.GetById(request.Id, cancellationToken);
        if (film is null)
            return Result.Fail(new ApplicationError("this film doesn't exist"));

        var namedFileImages = request.Images.Select(i => new NamedFileImage(Guid.NewGuid().ToString(), i)).ToList();

        var paths = fileRepository.SaveFilmImage(namedFileImages, film.Name);
        var filmImages = paths.Select(p => new FilmImage { Id = Guid.NewGuid(), FilmId = film.Id, Uri = p }).ToList();

        await filmRepository.NewImages(filmImages,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new NewFilmsImagesCommandResponse(paths));
    }
}
