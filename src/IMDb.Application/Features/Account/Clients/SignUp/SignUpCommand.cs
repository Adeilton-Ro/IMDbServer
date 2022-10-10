using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Account.Clients.SignUp;
public record SignUpCommand(string Email, string Name, string Password) : IRequest<Result<SignUpCommandResponse>>;