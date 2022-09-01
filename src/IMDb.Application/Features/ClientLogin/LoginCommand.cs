using FluentResults;
using MediatR;

namespace IMDb.Application.Features.ClientLogin;
public record LoginCommand(string Email, string Password) : IRequest<Result<LoginCommandResponse>>;