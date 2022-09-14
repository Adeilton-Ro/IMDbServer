using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Films.NewGender;
public record NewGenderCommand(string Name, string Description) : IRequest<Result<NewGenderCommandResponse>>;