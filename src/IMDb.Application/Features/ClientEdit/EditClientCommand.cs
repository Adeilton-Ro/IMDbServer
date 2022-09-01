using FluentResults;
using MediatR;

namespace IMDb.Application.Features.ClientEdit;
public record EditClientCommand(Guid Id, string Name, string Email, string Password) : IRequest<Result>;