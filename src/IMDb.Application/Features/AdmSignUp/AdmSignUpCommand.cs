using FluentResults;
using MediatR;

namespace IMDb.Application.Features.AdmSignUp;
public record AdmSignUpCommand(string Name, string Email, string Password) : IRequest<Result<AdmSignUpCommandResponse>>;
