using FluentResults;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using IMDb.Infra.FileSystem.Abstraction.Interfaces.FileRepositories;
using MediatR;

namespace IMDb.Application.Features.Film.NewDirector;
public class NewDirectorCommandHandler : IRequestHandler<NewDirectorCommand, Result<NewDirectorCommandResponse>>
{
    private readonly IDirectorRepository directorRepository;
    private readonly IFileRepository fileRepository;
    private readonly IUnitOfWork unitOfWork;

    public NewDirectorCommandHandler(IDirectorRepository directorRepository, IFileRepository fileRepository, IUnitOfWork unitOfWork)
    {
        this.directorRepository = directorRepository;
        this.fileRepository = fileRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Result<NewDirectorCommandResponse>> Handle(NewDirectorCommand request, CancellationToken cancellationToken)
    {
        var director = new Director
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        director.UrlImage = fileRepository.SaveDirectorImages(request.Image, director.Id.ToString());

        await directorRepository.Create(director, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new NewDirectorCommandResponse(director.Id));
    }
}