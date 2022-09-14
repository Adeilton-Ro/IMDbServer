using FluentResults;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using IMDb.Infra.FileSystem.Abstraction.Interfaces.FileRepositories;
using MediatR;

namespace IMDb.Application.Features.Films.NewActor;
public class NewActorCommandHandler : IRequestHandler<NewActorCommand, Result<NewActorCommandResponse>>
{
    private readonly ICastRepository<Actor> actorRepository;
    private readonly IFileRepository fileRepository;
    private readonly IUnitOfWork unitOfWork;

    public NewActorCommandHandler(ICastRepository<Actor> actorRepository, IFileRepository fileRepository, IUnitOfWork unitOfWork)
    {
        this.actorRepository = actorRepository;
        this.fileRepository = fileRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Result<NewActorCommandResponse>> Handle(NewActorCommand request, CancellationToken cancellationToken)
    {
        var actor = new Actor
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
        };

        actor.UrlImage = fileRepository.SaveActorImage(request.Image, actor.Id.ToString());

        await actorRepository.Create(actor, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return Result.Ok(new NewActorCommandResponse(actor.Id));
    }
}