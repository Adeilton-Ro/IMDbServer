using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Film.NewGender;
public record NewGenderCommand(string Name) : IRequest<Result<NewGenderCommandResponse>>;