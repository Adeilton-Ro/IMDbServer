using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Account.Adms.SignUp;
public record AdmSignUpCommand(string Name, string Email, string Password) : IRequest<Result<AdmSignUpCommandResponse>>;
