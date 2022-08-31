using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Login;
public record LoginCommand(string Email, string Password) : IRequest<Result<LoginCommandResponse>>;