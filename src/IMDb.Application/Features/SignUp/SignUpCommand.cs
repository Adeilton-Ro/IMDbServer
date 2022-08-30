using FluentResults;
using MediatR;

namespace IMDb.Application.Features.SignUp;
public record SignUpCommand(string Email, string Name, string Password) : IRequest<Result<SignUpCommandResponse>>;