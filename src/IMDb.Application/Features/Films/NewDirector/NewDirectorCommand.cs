using FluentResults;
using IMDb.Infra.FileSystem.Abstraction;
using MediatR;

namespace IMDb.Application.Features.Films.NewDirector;
public record NewDirectorCommand : IRequest<Result<NewDirectorCommandResponse>>
{
    public string Name { get; set; }
    public FileImage Image { get; set; }
    public NewDirectorCommand() { }
    public NewDirectorCommand(string name, FileImage image)
    {
        Name = name;
        Image = image;
    }
}