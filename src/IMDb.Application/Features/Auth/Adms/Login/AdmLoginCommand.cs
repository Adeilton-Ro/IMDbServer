using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Auth.Adms.Login;
public record AdmLoginCommand(string Email, string Password) : IRequest<Result<AdmLoginCommandResponse>>;