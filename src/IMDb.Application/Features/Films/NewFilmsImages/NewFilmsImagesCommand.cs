using FluentResults;
using IMDb.Infra.FileSystem.Abstraction;
using MediatR;

namespace IMDb.Application.Features.Films.NewFilmsImages;
public record NewFilmsImagesCommand : IRequest<Result<NewFilmsImagesCommandResponse>>
{
	public Guid Id { get; set; }
	public IEnumerable<FileImage> Images { get; set; }
	public NewFilmsImagesCommand(Guid id, IEnumerable<FileImage> images)
	{
		Id = id;
		Images = images;
	}
}
