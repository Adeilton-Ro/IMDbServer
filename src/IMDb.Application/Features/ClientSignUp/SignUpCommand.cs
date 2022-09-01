using FluentResults;
using MediatR;

namespace IMDb.Application.Features.ClientSignUp;
public record SignUpCommand(string Email, string Name, string Password) : IRequest<Result<SignUpCommandResponse>>;