using FluentResults;
using IMDb.Infra.FileSystem.Abstraction;
using MediatR;

namespace IMDb.Application.Features.Films.NewActor;
public record NewActorCommand : IRequest<Result<NewActorCommandResponse>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public FileImage Image { get; set; }
    public NewActorCommand() { }
    public NewActorCommand(string name, string description, FileImage image)
    {
        Name = name;
        Description = description;
        Image = image;
    }
}